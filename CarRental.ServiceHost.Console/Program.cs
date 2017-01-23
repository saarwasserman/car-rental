using CarRental.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM = System.ServiceModel;
using System.Timers;
using CarRental.Business.Entities;
using System.Transactions;
using System.Security.Principal;
using System.Threading;
using Core.Common.Core;
using CarRental.Business.Bootstrapper;

namespace CarRental.ServiceHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GenericPrincipal principal = new GenericPrincipal(new GenericIdentity("Saar"), new string[] { "CarAdminRole" });
            Thread.CurrentPrincipal = principal;

            ObjectBase.Container = MEFLoader.Init();

            Console.WriteLine("Starting up services...");
            Console.WriteLine("");

            SM.ServiceHost hostInventoryManager = new SM.ServiceHost(typeof(InventoryManager));
            SM.ServiceHost hostRentalManager = new SM.ServiceHost(typeof(RentalManager));
            SM.ServiceHost hostAccountManager = new SM.ServiceHost(typeof(AccountManager));

            StartService(hostInventoryManager, "InventoryManager");
            StartService(hostRentalManager, "RentalManager");
            StartService(hostAccountManager, "AccountManager");

            System.Timers.Timer timer = new System.Timers.Timer(10000);
            timer.Elapsed += onTimerElapsed;
            timer.Start();

            Console.WriteLine("Reservation monitor started");

            Console.WriteLine("");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();

            timer.Stop();

            Console.WriteLine("Reservation monitor stopped");

            StopService(hostInventoryManager, "InventoryManager");
            StopService(hostRentalManager, "RentalManager");
            StopService(hostAccountManager, "AccountManager");

        }

        public static void onTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Looking for dead reservations at {0}.", DateTime.Now.ToString());

            RentalManager rentalManager = new RentalManager();

            Reservation[] reservations = rentalManager.GetDeadReservations();
            if (reservations != null)
            {
                foreach (Reservation reservation in reservations)
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        try
                        {
                            rentalManager.CancelReservation(reservation.ReservationId);
                            Console.WriteLine("Canceling reservation '{0}'", reservation.ReservationId);
                            scope.Complete();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("There was an exception when trying to cancel reservation {0}", reservation.ReservationId);
                        }
                    }

                }
            }

        }

        public static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            Console.WriteLine("Service {0} started.", serviceDescription);

            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine("Listening on endpoint:");
                Console.WriteLine("Address: {0}", endpoint.Address.Uri);
                Console.WriteLine("Binding: {0}", endpoint.Binding.Name);
                Console.WriteLine("Contract: {0}", endpoint.Contract.Name);
            }

            Console.WriteLine("");
        }

        public static void StopService(SM.ServiceHost host, string serviceDescription)
        {
            // Not abort in order to serve pending calls
            host.Close();
            Console.WriteLine("Service {0} stopped.", serviceDescription);
        }
    }
}
