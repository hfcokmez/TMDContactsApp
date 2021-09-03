using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IContactService
    {
        /// <summary>
        /// Gets the Contact object with the given Contact Id parameter. 
        /// </summary>
        /// <param name="contactId">An integer value bigger than 0.</param>
        /// <returns>Returns an object of DataResult asynchronously.</returns>
        Task<IDataResult<Contact>> GetByIdAsync(int contactId);
        /// <summary>
        /// Gets all of the Contacts in a List.
        /// </summary>
        /// <returns>Asynchronously returns an object of DataResult which contains List of Contact's.</returns>
        Task<IDataResult<List<Contact>>> GetListAsync();
        /// <summary>
        /// Gets the User's Contact List with pagination by the given User Id, Page Number and Page Size Parameters.
        /// </summary>
        /// <param name="userId">An integer value which should be bigger than 0.</param>
        /// <param name="pageNumber">Page number of result page.</param>
        /// <param name="pageSize">Size of the result page.</param>
        /// <returns>Asynchronously returns a DataResult object, containing selected part of the User's Contact List.</returns>
        Task<IDataResult<PaginationDto<Contact>>> GetListByUserIdAsync(int userId, int pageNumber, int pageSize);
        /// <summary>
        /// Gets the User's Contact List with the given User Id, Page Number and Page Size Parameters.
        /// </summary>
        /// <param name="userId">An integer value which required to access the User's Contacts.</param>
        /// <returns>Asynchronously returns a DataResult object, containing part of the User's Contact List.</returns>
        Task<IDataResult<List<Contact>>> GetListByUserIdAsync(int userId);
        /// <summary>
        /// Checks the User is in the Contact's list.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tel"></param>
        /// <returns>Asynchronously returns a boolean value depending on whether the User is in the Contact's list</returns>
        Task<IDataResult<Contact>> IsUserInContactsListAsync(int userId, string tel);
        /// <summary>
        /// Adds the given Contact according to the UserId parameter inside the Contact Object.
        /// </summary>
        /// <param name="contact">Contact entity object.</param>
        /// <returns>Asynchronously returns a Result object, containing Message and Success information of the operation.</returns>
        Task<IResult> AddAsync(Contact contact);
        /// <summary>
        /// Adds the given Contact to the User's Contact List. If the User is in the list of the Contact they are trying to add, it synchronizes the Contact information with the information of the pre-registered User.
        /// </summary>
        /// <param name="contact">Contact entity object.</param>
        /// <returns>Asynchronously returns a Result object, containing Message and Success information of the operation.</returns>
        Task<IResult> AddWithAsync(Contact contact);
        /// <summary>
        /// Deletes the given Contact according to the Id parameter inside the Contact Object.
        /// </summary>
        /// <param name="contact">Contact entity object.</param>
        /// <returns>Asynchronously returns a Result object, containing Message and Success information of the operation.</returns>
        Task<IResult> DeleteAsync(int contact);
        /// <summary>
        /// Deletes the given List of Contact's according to the Id parameter inside the List of Contact Object.
        /// </summary>
        /// <param name="contacts">Contact Id List object.</param>
        /// <returns>Asynchronously returns a Result object, containing Message and Success information of the operation.</returns>
        Task<IResult> DeleteListAsync(List<int> contacts);
        /// <summary>
        /// Updates the given Contact according to the Id parameter inside the Contact Object.
        /// </summary>
        /// <param name="contact">Contact entity object.</param>
        /// <returns>Asynchronously returns a Result object, containing Message and Success information of the operation.</returns>
        Task<IResult> UpdateAsync(Contact contact);
    }
}
