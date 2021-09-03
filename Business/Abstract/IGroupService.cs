using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGroupService
    {
        /// <summary>
        /// Gets the Group object with the given Group Id parameter. 
        /// </summary>
        /// <param name="groupId">An integer value bigger than 0.</param>
        /// <returns>Returns an object of DataResult asynchronously.</returns>
        Task<IDataResult<Group>> GetByIdAsync(int groupId);
        /// <summary>
        /// Gets all of the Groups in a List object.
        /// </summary>
        /// <returns>Asynchronously returns an object of DataResult which contains List of Contact's.</returns>
        Task<IDataResult<List<Group>>> GetListAsync();
        /// <summary>
        /// Gets the User's Group List with the given User Id, Page Number and Page Size Parameters.
        /// </summary>
        /// <param name="userId">An integer value which should be bigger than 0.</param>
        /// <returns>Asynchronously returns a DataResult object, containing the User's Group List.</returns>
        Task<IDataResult<List<Group>>> GetListAsync(int userId);
        /// <summary>
        /// Asynchronously adds the given Group according to the UserId parameter inside the Group Object.
        /// </summary>
        /// <param name="group">Group entity object.</param>
        /// <returns>Asynchronously returns a Result object, containing Message and Success information of the operation.</returns>
        Task<IResult> AddAsync(Group group);
        /// <summary>
        /// Asynchronously deletes the given Group according to the Id parameter inside the Group Object.
        /// </summary>
        /// <param name="group">Group entity object.</param>
        /// <returns>Asynchronously returns a Result object, containing Message and Success information of the operation.</returns>
        Task<IResult> DeleteAsync(int group);
        /// <summary>
        /// Asynchronously deletes the given List of Group's according to the Id parameter inside the List of Group Object.
        /// </summary>
        /// <param name="groups">Group entity object.</param>
        /// <returns>Asynchronously returns a Result object, containing Message and Success information of the operation.</returns>
        Task<IResult> DeleteListAsync(List<int> groups);
        /// <summary>
        /// Asynchronously updates the given Group according to the Id parameter inside the Group Object.
        /// </summary>
        /// <param name="group">Group entity object.</param>
        /// <returns>Asynchronously returns a Result object, containing Message and Success information of the operation.</returns>
        Task<IResult> UpdateAsync(Group group);
    }
}
