// Models/User.cs

// Models/User.cs

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReactServer.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }
        [NotMapped]
        public string ShortDateOfBirth { get; set; }

        public bool IsActive { get; set; }
        [NotMapped]
        public string IsActiveText { get; set; }

        public string Gender { get; set; }
    }

    public class UserApiModel
    {
        public List<ColumnModel> Columns { get; set; }
        public IEnumerable<User> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int PerPage { get; set; }
    }
    public class ColumnModel
    {
        public string AccessorKey { get; set; }
        public string Header { get; set; }
        public string EditVariant { get; set; }
        public string ColumnDefType { get; set; }
        public bool EnableEditing { get; set; } = true;
        public bool EnableClickToCopy { get; set; } = false;
        public string Type { get; set; }
        public List<SelectListItem> EditSelectOptions { get; set; }
        public MuiEditTextFieldPropsModel MuiEditTextFieldProps { get; set; }
    }

    public class MuiEditTextFieldPropsModel
    {
        public bool Required { get; set; }
        public string Error { get; set; }
        public string HelperText { get; set; }
        public bool Select { get; set; }
    }

    public class Pagination<T>(int pageSize, int pageIndex)
    {
        public int PageSize { get; set; } = pageSize;
        public int PageIndex { get; set; } = pageIndex;

        public async Task<PagedResult<T>> Paginate(IEnumerable<T> source)
        {
            var totalCount = source.Count();
            var pagedData = source.Skip(PageSize * PageIndex).Take(PageSize);

            return await Task.FromResult(new PagedResult<T>
            {
                Items = pagedData,
                TotalCount = totalCount,
                PerPage = PageSize
            });
        }
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PerPage { get; set; }
    }


}
