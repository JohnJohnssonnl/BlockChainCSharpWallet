﻿using System;

namespace SHCWalletC
{
    class LoginManager
    {
        String      WalletName;
        String      passString;
        Boolean     MustCreateWallet;

        public string ParmWalletUserName(string _WalletName)
        {
            WalletName = _WalletName;

            return WalletName;
        }
        public string ParmPassCode(string _passCode)
        {
            passString = _passCode;

            return passString;
        }
        public Boolean ParmCreateNewWallet(Boolean _MustCreateWallet)
        {
            MustCreateWallet = _MustCreateWallet;

            return MustCreateWallet;
        }

        public Boolean Login()
        { 
            string StoreString = "";
            Boolean passAccepted;

            if (WalletName == "")
            {
                return false;
            }
            string WalletFilePath = AppDomain.CurrentDomain.BaseDirectory + @"bin\\" + WalletName + ".dat";

            if (!WalletFileManager.DoesWalletExist(WalletFilePath) && MustCreateWallet)
            {
                return GenerateWallet.NewWallet(WalletName, WalletFilePath, passString);
            }
            else if (WalletFileManager.DoesWalletExist(WalletFilePath) && MustCreateWallet)
            {
                return false;
            }
            else if (!WalletFileManager.DoesWalletExist(WalletFilePath))
            {
                return false;
            }
            else
            { 
                //Login
                StoreString = WalletFileManager.ReadBin(WalletFilePath);

                //Checking passWord
                passAccepted = PasswordManager.CheckPass(passString, StoreString);

                passString = "";    //Dispose of the pass, we don't want any traces left
                if (!passAccepted)
                {
                    System.Threading.Thread.Sleep(1 * 1000);
                }

                return passAccepted;
            }
        }
    }
}
