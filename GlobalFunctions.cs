﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace helpmebot6
{
    public class GlobalFunctions
    {

        /// <summary>
        /// Searches the array haystack for needle
        /// </summary>
        /// <param name="needle"></param>
        /// <param name="haystack"></param>
        /// <returns>true if the needle is in the haystack</returns>
        public static bool isInArray( string needle, string[ ] haystack )
        {
            foreach ( string straw in haystack )
            {
                if ( needle == straw )
                    return true;
            }

            return false;
        }

        public static string popFromFront( ref string[ ] list )
        {
            string firstItem = list[ 0 ];
            list = string.Join( " ", list, 1, list.Length - 1 ).Split( ' ' );
            return firstItem;
        }

        public static void ErrorLog( Exception ex, MethodBase method )
        {
            Console.WriteLine( "*********************************" );
            Console.WriteLine( "Error detected in method " + method.Module + "::" + method.Name );
            Console.WriteLine( ex.ToString( ) + ex.StackTrace );
            IAL.singleton.IrcPrivmsg( Helpmebot6.debugChannel, ex.Message );
            Console.WriteLine( "*********************************" );
            
        }

        public static string escape( string str )
        {
            return MySql.Data.MySqlClient.MySqlHelper.EscapeString( str );
        }
    }
}
