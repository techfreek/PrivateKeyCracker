using System;
using System.ComponentModel;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateKeyCracker {
    class Program
    {
        private static UInt64 p, q, d, n;
        private static int e;
        static void Main(string[] args)
        {
            e = 49;
            n = 10539750919;
            String cipher = "ITG!AAEXEX IRRG!IGRXI OIXGEREAGO";
            CrackPQ(e, n);
            CrackD(e);
            Decipher(cipher);
            Console.ReadLine();
        }

        static void CrackPQ(int e, UInt64 n)
        {
            UInt64 t;

            for (UInt64 i = 1; i < n; i++)
            {
                p = i;
                q = n/p;
                if (n == (p*q))
                {
                    t = (p - 1)*(q - 1);
                    if (BigInteger.GreatestCommonDivisor(e, t) == 1)
                    {
                        Console.WriteLine("P: {0}, Q: {1}", p, 1);
                        return;
                    }
                }
            }
        }

        static void CrackD(int e)
        {
            UInt64 eUlong = (UInt64) e;
            UInt64 pqMinus = (p - 1)*(q - 1);
            for (d = 1; d < UInt64.MaxValue; d++)
            {
                if (((eUlong*d)%pqMinus) == 1)
                {
                    break;
                }
            }

            Console.WriteLine("D is: {0}", d);
        }

        static void Decipher(string cipher)
        {
            int i = 0;
            UInt64 temp = 0;
            int length = 0;
            int mult = 0;
            string[] chunks = cipher.Split(' ');
            UInt64 nUlong = n;
            foreach(string chunk in chunks)
            {
                length = chunk.Length -1;
                for (i = length; i >= 0; i--)
                {
                    mult = (length - i);
                    temp += (UInt64)(CharMap(chunk[i])) * (UInt64)Math.Pow(10, mult);
                }

                Console.WriteLine(temp.ToString());
                
                //temp = Convert.ToUInt64(BigInteger.Pow(temp, d)) % n;

                //string tempstr = Console.ReadLine();
                /*char[] tempchars = tempstr.ToCharArray();
                for (i = 0; i < tempstr.Length; i++)
                {
                    tempchars[i] = CharMap(Convert.ToInt32(tempchars[i]));
                    Console.Write(tempchars[i]);
                }*/
                //tempstr = tempchars.ToString();
                //Console.Write("{0} ", tempstr);
            }
            
        }

        static int CharMap(char a)
        {
            int r = 0;
            switch (a)
            {
                case 'A':
                    r = 1;
                    break;
                case 'E':
                    r = 2;
                    break;
                case 'G':
                    r = 3;
                    break;
                case 'I':
                    r = 4;
                    break;
                case 'O':
                    r = 5;
                    break;
                case 'R':
                    r = 6;
                    break;
                case 'T':
                    r = 7;
                    break;
                case 'X':
                    r = 8;
                    break;
                case '!':
                    r = 9;
                    break;
                case '0':
                    r = 10;
                    break;
                default:
                    r = 11;
                    break;
            }
            return r;
        }

        static char CharMap(int a) {
            char r = ' ';
            switch (a) {
                case 1:
                    r ='A';
                    break;
                case 2:
                    r = 'E';
                    break;
                case 3:
                    r = 'G';
                    break;
                case 4:
                    r = 'I';
                    break;
                case 5:
                    r = 'O';
                    break;
                case 6:
                    r = 'R';
                    break;
                case 7:
                    r = 'T';
                    break;
                case 8:
                    r = 'X';
                    break;
                case 9:
                    r = '!';
                    break;
                case 10:
                    r = '0';
                    break;
                default:
                    r = ' ';
                    break;
            }
            return r;
        }
    }
}
