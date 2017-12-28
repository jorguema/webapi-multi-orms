using Infrastructure.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Transactions;

namespace Infrastructure.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public Contact GetById(int id)
        {
            var sql = @"SELECT * FROM Contacts WHERE Id = @Id;
                        SELECT * FROM Addresses WHERE ContactId = @Id";

            using (var multipleResults = this.db.QueryMultiple(sql, new { id }))
            {
                var contact = multipleResults.Read<Contact>().SingleOrDefault();
                var addresses = multipleResults.Read<Address>().ToList();

                if(contact != null)
                {
                    contact.Addresses = addresses;
                }

                return contact;
            }            
        }

        public IEnumerable<Contact> GetAll()
        {
            return this.db.Query<Contact>("SELECT * FROM Contacts");
        }

        public Contact Add(Contact contact)
        {
            var sql = @"INSERT INTO Contacts(FirstName, LastName, Email, Company, Title)
                        VALUES(@FirstName, @LastName, @Email, @Company, @Title);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            var id =  this.db.Query<int>(sql, contact).Single();
            contact.Id = id;
            return contact;
        }

        public void Delete(int id)
        {
            var sql = @"DELETE FROM Contacts WHERE Id = @Id";

            this.db.Execute(sql, new {id});
        }

        public Contact Save(Contact contact)
        {
            using (var txScope = new TransactionScope())
            {
                if (contact.IsNew)
                    Add(contact);
                //else
                //update
                foreach (var address in contact.Addresses)
                {
                    address.ContactId = contact.Id;

                    if (address.IsNew)
                        Add(address);
                    //else
                    //update
                }

                txScope.Complete();
            }

            return contact;
           
        }

        public Address Add(Address address)
        {
            var sql = @"INSERT INTO Addresses(ContactId, AddressType, StreetAddress, City, StateId, PostalCode)
                        VALUES(@ContactId, @AddressType, @StreetAddress, @City, @StateId, @PostalCode);
                        SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = this.db.Query<int>(sql, address).Single();
            address.Id = id;
            return address;
        }
    }
}
