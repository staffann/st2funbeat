/*
Copyright (C) 2009, 2010 Jan Ohlson

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Janohl.ST2Funbeat.Settings
{
    [Serializable]
    public class UserSettings
    {
        private bool boLoginIdNeedsUpdating = true;
        private bool boLoginSecretNeedsUpdating = true;
        private string userName="", password="";
        private string loginId="", loginSecret="";
        
        public string Username
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                boLoginIdNeedsUpdating = true;
                boLoginSecretNeedsUpdating = true;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = FunbeatDll.FunbeatEncryption.EncryptPassword(value);
                boLoginIdNeedsUpdating = true;
                boLoginSecretNeedsUpdating = true;
            }
        }
        public string HashedPassword
        {
            set
            {
                password = value;
                boLoginIdNeedsUpdating = true;
                boLoginSecretNeedsUpdating = true;
            }
        }

        public string LoginId
        {
            get
            {
                if (boLoginIdNeedsUpdating || boLoginSecretNeedsUpdating)
                {
                    // Encrypt password
                    // Call ValidateAndCreateSecrets
                    // Hash loginSecret
                    if (FunbeatService.CreateLogin(userName, password, out loginId, out loginSecret))
                    {
                        boLoginIdNeedsUpdating = false;
                        boLoginSecretNeedsUpdating = false;
                    }
                    else
                    {
                        return null;
                    }
                }
                return loginId;

            }
            set
            {
                loginId = value;
                if(loginId != "")
                    boLoginIdNeedsUpdating = false;
            }
        }

        public string LoginSecret
        {
            get
            {
                if (boLoginIdNeedsUpdating || boLoginSecretNeedsUpdating)
                {
                    // Encrypt password
                    // Call ValidateAndCreateSecrets
                    // Hash loginSecret
                    if (FunbeatService.CreateLogin(userName, password, out loginId, out loginSecret))
                    {
                        boLoginIdNeedsUpdating = false;
                        boLoginSecretNeedsUpdating = false;
                    }
                    else
                    {
                        return null;
                    }
                }
                return loginSecret;

            }
            set
            {
                loginSecret = value;
                if(loginSecret != "")
                    boLoginSecretNeedsUpdating = false;
            }
        }
    
    }
}
