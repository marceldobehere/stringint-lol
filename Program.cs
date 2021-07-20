using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stringint_for_anar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number A:");
            Strint a = new Strint(Console.ReadLine());

            Console.WriteLine("Enter number B:");
            Strint b = new Strint(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine("A + B = " + Strint.Get_String(a + b));
            Console.WriteLine("A - B = " + Strint.Get_String(a - b));
            Console.WriteLine("A * B = " + Strint.Get_String(a * b));
            Console.WriteLine("A / B = " + Strint.Get_String(a / b));
            Console.WriteLine();
            Console.WriteLine("A % B = " + Strint.Get_String(a % b));
            Console.WriteLine();
            Console.WriteLine($"A > B ? = {a > b}");
            Console.WriteLine($"A < B ? = {a < b}");
            Console.ReadLine();
        }
    }
    class Strint
    {
        public string value;
        public Strint(string val)
        {
            if (val.Length > 0)
            {
                value = val;

                if (val[0] == '-')
                {
                    negative = true;
                    value = value.Substring(1, value.Length - 1);
                }
                else
                {
                    negative = false;
                }
            }
            else
            {
                value = "0";
                negative = false;
            }
        }
        public bool negative;

        public static Strint Invert_negative(Strint input)
        {
            Strint outs;
            if (input.negative)
            {
                outs = new Strint(input.value);
            }
            else
            {
                outs = new Strint("-" + input.value);
            }
            return outs;
        }

        public static int[] Get_int_array(Strint input)
        {
            int[] data = new int[input.value.Length];
            // 0 -> 48
            // 9 -> 57
            for (int i = 0; i < input.value.Length; i++)
            {
                data[i] = (((int)input.value[i]) - 48);
            }

            return data;
        }


        public static int[] Get_int_array(int extra, Strint input)
        {
            int[] data = new int[input.value.Length + extra];
            // 0 -> 48
            // 9 -> 57
            for (int i = 0; i < input.value.Length; i++)
            {
                data[i + extra] = (((int)input.value[i]) - 48);
            }

            return data;
        }
        public static int[] Get_int_array(int max, int extra, Strint input)
        {
            int[] data = new int[max];
            // 0 -> 48
            // 9 -> 57
            extra += max - input.value.Length - 1;
            for (int i = 0; i < input.value.Length; i++)
            {
                data[i + extra] = (((int)input.value[i]) - 48);
            }

            return data;
        }

        public static Strint Get_Strint_from_int_array(int[] input, bool minus)
        {
            string a;
            if (minus)
            {
                a = "-";
            }
            else
            {
                a = "";
            }
            bool still0 = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != 0)
                {
                    still0 = false;
                }
                if (!still0)
                {
                    a += input[i];
                }
            }

            return new Strint(a);
        }


        public static string Get_String(Strint input)
        {
            string a = "";


            bool still0 = true;
            for (int i = 0; i < input.value.Length; i++)
            {
                if (input.value[i] != '0')
                {
                    still0 = false;
                }
                if (!still0)
                {
                    a += input.value[i];
                }
            }

            if (a != "")
            {
                if (input.negative && input.value != "0")
                {
                    a = "-" + a;
                }
                return a;
            }
            else
            {
                return "0";
            }
        }


        public static bool operator >(Strint left, Strint right)
        {
            if (!left.negative && !right.negative)
            {
                int max = Math.Max(left.value.Length, right.value.Length);
                int[] up = Strint.Get_int_array(max, 1, left);
                int[] down = Strint.Get_int_array(max, 1, right);
                bool bigger = false;
                for (int i = 0; i < max; i++)
                {
                    if (up[i] > down[i])
                    {
                        bigger = true;
                        break;
                    }
                    if (up[i] < down[i])
                    {
                        bigger = false;
                        break;
                    }
                }


                return bigger;
            }
            else
            {
                if (!left.negative && right.negative)
                {
                    return true;
                }
                else
                {
                    if (left.negative && !right.negative)
                    {
                        return false;
                    }
                    else
                    {
                        return Strint.Invert_negative(right) > Strint.Invert_negative(left);
                    }
                }
            }
        }


        public static bool operator <(Strint left, Strint right)
        {
            if (!left.negative && !right.negative)
            {
                int max = Math.Max(left.value.Length, right.value.Length);
                int[] up = Strint.Get_int_array(max, 1, right);
                int[] down = Strint.Get_int_array(max, 1, left);
                bool bigger = false;
                for (int i = 0; i < max; i++)
                {
                    if (up[i] > down[i])
                    {
                        bigger = true;
                        break;
                    }
                    if (up[i] < down[i])
                    {
                        bigger = false;
                        break;
                    }
                }


                return bigger;
            }
            else
            {
                if (!left.negative && right.negative)
                {
                    return false;
                }
                else
                {
                    if (left.negative && !right.negative)
                    {
                        return true;
                    }
                    else
                    {
                        return Strint.Invert_negative(right) < Strint.Invert_negative(left);
                    }
                }
            }
        }



        public static Strint operator +(Strint left, Strint right)
        {
            if (left.negative && right.negative)
            {
                return Strint.Invert_negative(Strint.Invert_negative(left) + Strint.Invert_negative(right));
            }
            else
            {
                if (!left.negative && right.negative)
                {
                    return left - Strint.Invert_negative(right);
                }
                else
                {
                    if (left.negative && !right.negative)
                    {
                        return right - Strint.Invert_negative(left);
                    }
                    else
                    {
                        int max = Math.Max(left.value.Length, right.value.Length) + 1;
                        int[] up = Strint.Get_int_array(max, 1, left);
                        int[] down = Strint.Get_int_array(max, 1, right);

                        for (int i = max - 1; i >= 0; i--)
                        {
                            down[i] += up[i];
                            if (down[i] > 9)
                            {
                                up[i - 1] += 1;
                                down[i] -= 10;
                            }
                        }



                        return Strint.Get_Strint_from_int_array(down, false);
                    }
                }
            }
        }



        public static Strint operator -(Strint left, Strint right)
        {
            if (left.negative && right.negative)
            {
                return Strint.Invert_negative(right) - left;
            }
            else
            {
                if (!left.negative && right.negative)
                {
                    return left + Strint.Invert_negative(right);
                }
                else
                {
                    if (left.negative && !right.negative)
                    {
                        return Strint.Invert_negative(Strint.Invert_negative(left) + right);
                    }
                    else
                    {
                        if (left > right)
                        {
                            int max = Math.Max(left.value.Length, right.value.Length) + 1;
                            int[] up = Strint.Get_int_array(max, 1, left);
                            int[] down = Strint.Get_int_array(max, 1, right);

                            for (int i = max - 1; i >= 0; i--)
                            {
                                up[i] -= down[i];
                                if (up[i] < 0)
                                {
                                    up[i - 1] -= 1;
                                    up[i] += 10;
                                }
                            }
                            return Strint.Get_Strint_from_int_array(up, false);
                        }
                        else
                        {
                            int max = Math.Max(left.value.Length, right.value.Length) + 1;
                            int[] up = Strint.Get_int_array(max, 1, right);
                            int[] down = Strint.Get_int_array(max, 1, left);

                            for (int i = max - 1; i >= 0; i--)
                            {
                                up[i] -= down[i];
                                if (up[i] < 0)
                                {
                                    up[i - 1] -= 1;
                                    up[i] += 10;
                                }
                            }
                            return Strint.Invert_negative(Strint.Get_Strint_from_int_array(up, false));
                        }
                    }
                }
            }
        }




        public static Strint operator *(Strint left, Strint right)
        {
            if (left.value.Length == 1)
            {
                Strint zero = new Strint("0");
                Strint one = new Strint("1");
                Strint sum = new Strint("0");

                if (left.negative)
                {
                    right = Strint.Invert_negative(right);
                    left = Strint.Invert_negative(left);
                }

                while (left > zero)
                {
                    sum = sum + right;
                    left = left - one;
                }

                return sum;
            }
            else
            {
                bool left_minus = left.negative;
                bool right_minus = right.negative;
                left = new Strint(left.value);
                right = new Strint(right.value);

                bool sum_negative = false;

                if ((left_minus || right_minus) && !(left_minus && right_minus))
                {
                    sum_negative = true;
                }


                Strint sum = new Strint("0");
                Strint ones = new Strint("");
                

                for (int add_zeroes = 0; add_zeroes < left.value.Length; add_zeroes++)
                {
                    ones = new Strint("" + left.value[left.value.Length - add_zeroes - 1]);
                    sum += new Strint((ones * right).value + "".PadLeft(add_zeroes, '0'));
                }

                if (sum_negative)
                {
                    sum = Strint.Invert_negative(sum);
                }

                return sum;
            }
        }










        public static Strint operator /(Strint left, Strint right)
        {
            bool left_minus = left.negative;
            bool right_minus = right.negative;
            left = new Strint(left.value);
            right = new Strint(right.value);

            bool sum_negative = false;

            if ((left_minus || right_minus) && !(left_minus && right_minus))
            {
                sum_negative = true;
            }

            Strint one = new Strint("1");
            if ((left + one) > right)
            {

                string output = "";
                string subdata;
                Strint subvalue = new Strint("0");
                int i;
                int counter;

                int step = 0;
                while (left.value.Length - step > 0)
                {
                    subdata = Strint.Get_String(subvalue) + left.value.Substring(step, left.value.Length - step);

                    i = 1;
                    subvalue = new Strint(subdata.Substring(0, i));
                    while (subvalue < right && i < subdata.Length)
                    {
                        i++;
                        subvalue = new Strint(subdata.Substring(0, i));
                    }
                    for (int x = 2; x < i; x++)
                    {
                        output += "0";
                    }

                    counter = 0;
                    while (subvalue > right)
                    {
                        subvalue -= right;
                        counter++;
                    }
                    if ((subvalue + one) > right)
                    {
                        subvalue -= right;
                        counter++;
                    }
                    output += counter;
                    step += i - 1;
                }

                //Console.WriteLine(output);
                Strint result = new Strint(output);

                if (sum_negative)
                {
                    result = Strint.Invert_negative(result);
                }

                return result;
            }
            else
            {
                return new Strint("0");
            }
        }




        public static Strint operator %(Strint left, Strint right)
        {
            bool left_minus = left.negative;
            bool right_minus = right.negative;
            left = new Strint(left.value);
            right = new Strint(right.value);

            bool sum_negative = false;

            if ((left_minus || right_minus) && !(left_minus && right_minus))
            {
                sum_negative = true;
            }

            Strint one = new Strint("1");
            if ((left + one) > right)
            {

                string output = "";
                string subdata;
                Strint subvalue = new Strint("0");
                int i;
                int counter;

                int step = 0;
                while (left.value.Length - step > 0)
                {
                    subdata = Strint.Get_String(subvalue) + left.value.Substring(step, left.value.Length - step);

                    i = 1;
                    subvalue = new Strint(subdata.Substring(0, i));
                    while (subvalue < right && i < subdata.Length)
                    {
                        i++;
                        subvalue = new Strint(subdata.Substring(0, i));
                    }
                    for (int x = 2; x < i; x++)
                    {
                        output += "0";
                    }

                    counter = 0;
                    while (subvalue > right)
                    {
                        subvalue -= right;
                        counter++;
                    }
                    if ((subvalue + one) > right)
                    {
                        subvalue -= right;
                        counter++;
                    }
                    output += counter;
                    step += i - 1;
                }

                Strint result = subvalue;

                if (sum_negative)
                {
                    result = Strint.Invert_negative(result);
                }

                return result;
            }
            else
            {
                return new Strint("0");
            }
        }
    }
}

