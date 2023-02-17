using System;

namespace MiniProject.Models
{
    public class MandatoryFields
    {
        private DateTime name = DateTime.Now;
        public DateTime CreatedDate
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        private DateTime? updatedDate = null;
        public DateTime? UpdatedDate
        {
            get
            {
                return updatedDate;
            }
            set
            {
                updatedDate = value;
            }
        }

        private bool isActive = true;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }

        public bool IsDeleted { get; set; }
    }
}
