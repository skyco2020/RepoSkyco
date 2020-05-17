using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Mailing
{
    public class RegisterUserStateMail : IStateMail
    {
        #region Members
        private String fullName;
        private String userName;
        private String userPass;
        private String[] usersMails;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fullName">Full name of the user</param>
        /// <param name="userName">User account name</param>
        /// <param name="userPass">account password</param>
        /// <param name="userMail">User mail</param>
        public RegisterUserStateMail(String fullName, String userName, String userPass, params String[] usersMails)
        {
            this.fullName = fullName;
            this.userName = userName;
            this.userPass = userPass;
            this.usersMails = usersMails;
        }
        #endregion

        #region Basic Functions
        public String Body()
        {          
            String body = @"

                    <html>
	                    <head>

	                    </head>

	                    <body style='width:600px;'>

		                    <div>
			                    <div>
				                    <img src='header image'>
				                    <br>
			                    </div>

			                    <div style='text-align:left; margin-left:25px; margin-top:10px'>
				                    <br>Welcome {0}.
				                    <br>
				                    <br>Thank you for registering at <b>SkyCo©</b>.
				                    <br>To continue, please confirm your account by clicking on the button below:
				                    <br>
			                    </div>

			                    <div style='text-align:left; margin-left:230px;'>
				                    <br>
				                    <a href='{3}'><img src='http://i.imgur.com/MNrJ1aj.png'></a>
				                    <br>
			                    </div>

			                    <div style='text-align:left; margin-left:25px;'>                                    
                                      If you don't see the image, please, <a href='{3}'>click here</a>
				                    <br>
				                    <br>Once you confirm the account you can log in with your data: 
				                    <br>- Username: {1}
				                    <br>- PassWord: {2}
				                    <br>
				                    <br>Thank you.
				                    <br>Equipment <b>SkyCo©</b>.
			                    </div>
			
			                    <div style='text-align:left; margin-top:25px;'>
				                    <img src='image footer'>
			                    </div>
		                    </div>

	                    </body>

                    </html>

                    ";
            String bodyToReturn = String.Format(body, this.fullName, this.userName, this.userPass,"");
            return bodyToReturn;
        }

        public String Subject()
        {           
            return MailConfiguration.GetInstance().AppSettings["company_name"] + " - Account validation";
        }

        public String[] To()
        {
            return this.usersMails;
        }
        #endregion
    }
}
