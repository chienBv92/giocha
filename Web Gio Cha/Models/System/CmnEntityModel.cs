using System;
using System.Collections.Generic;
using System;
using System.Data;
using System.Web.Mvc;

namespace Web_Gio_Cha.Models
{
	/// <summary>
	/// Common entity Model
	/// </summary>
    [Serializable] 
	public class CmnEntityModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CmnEntityModel"/> class.
		/// </summary>
		public CmnEntityModel()
		{
			this.Clear();
			this.CurrentScreenID = string.Empty;
			this.ParentScreenID = string.Empty;
			this.ScreenRoute = string.Empty;
		}

        public int USER_CITY { get; set; }
        public int USER_DISTRICT { get; set; }
        public int USER_TOWN { get; set; }

        public string CITY_NAME { get; set; }
        public string DISTRICT_NAME { get; set; }
        public string TOWN_NAME { get; set; }
        public string USER_ADDRESS { get; set; }
       

        public long ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email_Confirm { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> IsAdmin { get; set; }

		public string CurrentScreenID { get; set; }

		public string ParentScreenID { get; set; }

        public string ipAddress { get; set; }

        public string userAgent { get; set; }

        public string browserType { get; set; }

        public string browserVersion { get; set; }

        public string ScreenRoute { get; set; }

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public void Clear()
		{
            this.ID = 0;
            this.Email = string.Empty;
            this.UserName = string.Empty;
		}
	}
}