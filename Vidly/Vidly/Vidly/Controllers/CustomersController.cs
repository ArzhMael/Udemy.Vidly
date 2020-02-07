using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        #region DbContext
        // Par convention, créer un DbContext privé et l'initialiser dans le constructeur du controlleur.
        // Ensuite override la méthode Dispose();
        private ApplicationDbContext _context;
        
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        public ActionResult Index()
        {
            // Note : Ajout de la méthode Include() pour également charger les propriétés de navigation.
            //List<Models.Customer> customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(/*customers*/);
        }

        public ActionResult Create()
        {
            // Note: Récupère la liste "MembershipTypes" et l'ajoute au ViewModel afin de pouvoir la récupérer dans la Dropdown List du formulaire.
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new ViewModels.CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            // Note: Redirige vers la vue du formulaire.
            return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            // Note: Vérifie si l'utilisateur existe.
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Models.Customer customer)
        {
            // Vérifie si le formulaire envoyé est valide.
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            // Si le client renvoyé par le formulaire ne possède pas d'Id, le créer dans la base de donnée, sinon modifier celui-ci dans la base de donnée.
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDay = customer.BirthDay;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}