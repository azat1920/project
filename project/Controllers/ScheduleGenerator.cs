using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using project.Models;

namespace project.Controllers
{
    public class ScheduleGenerator
    {
        private int rest_count = 20;


        private List<Day> Generate(List<Cook> cooks, List<Qualification> qualifications, int count)
        {

            List<Day> days = new List<Day>();

            cooks = cooks.OrderByDescending(cook => cook.shift_type).OrderByDescending(cook => cook.schedule).OrderByDescending(cook => cook.hours).ToList();


            for (int i = 0; i < count; i++)
            {
                days.Add(new Day(rest_count){ id = i });
            }


            var cooks_morn = cooks.FindAll(cook => cook.shift_type.Equals("утренняя")).ToList();
            var cooks_morn_5 = cooks_morn.FindAll(cook => cook.schedule.Equals(@"5/2")).ToList();
            var cooks_morn_2 = cooks_morn.FindAll(cook => cook.schedule.Equals(@"2/2")).ToList();


            var cooks_even = cooks.FindAll(cook => cook.shift_type.Equals("вечерняя")).ToList();
            var cooks_even_5 = cooks_even.FindAll(cook => cook.schedule.Equals(@"5/2")).ToList();
            var cooks_even_2 = cooks_even.FindAll(cook => cook.schedule.Equals(@"2/2")).ToList();

             
            //Filling the schedule by cooks, 5/2
            foreach (var rest_day in days.ElementAt(0).list)
            {
                bool reversed = false;

                while (rest_day.count < 7 && cooks_morn_5.Count > 0)
                {
                    rest_day.cooks.Add(cooks_morn_5.ElementAt(0));
                    rest_day.count += cooks_morn_5.ElementAt(0).hours;
                    cooks_morn_5.RemoveAt(0);
                }

                if (!reversed && cooks_even_5.Count != 0 && rest_day.count >= 7)
                {
                    cooks_even_5 = cooks_even_5.OrderBy(cook => cook.hours).ToList();
                    reversed = true;
                }


                if (cooks_morn_5.Count == 0)
                {
                    if (reversed)
                    {
                        cooks_even_5 = cooks_even_5.OrderByDescending(cook => cook.hours).ToList();
                        reversed = false;
                    }

                    while (rest_day.count < 7 && cooks_even_5.Count > 0 && (cooks_even_5.ElementAt(0).hours + rest_day.count) < 14)
                    {
                        rest_day.cooks.Add(cooks_even_5.ElementAt(0));
                        rest_day.count += cooks_even_5.ElementAt(0).hours;
                        cooks_even_5.RemoveAt(0);
                    }
                }

                while (rest_day.count < 14 && cooks_even_5.Count > 0)
                {
                    rest_day.cooks.Add(cooks_even_5.ElementAt(0));
                    rest_day.count += cooks_even_5.ElementAt(0).hours;
                    cooks_even_5.RemoveAt(0);
                        
                }

                if (reversed)
                {
                    cooks_even_5 = cooks_even_5.OrderByDescending(cook => cook.hours).ToList();
                    reversed = false;
                }
                  
            }

            int j = 0;
            while (j < count)
            {
                
                for (int i = j; i < j + 5; i++)
                {
                    if (i >= count)
                    {
                        break;
                    }
                    if (i != 0)
                    {   
                        
                        for (int k = 0; k < days.ElementAt(0).list.Count; k++)
                        {
                            var restDay = days.ElementAt(0).list.ElementAt(k);
                            days[i].list[k] = new RestaurantDay(restDay);    
                        }
                        
                    }

                }

                j += 7;
            }

            //Filling the schedule by cooks, 2/2,  from 2-nd day
            foreach (var rest_day in days.ElementAt(1).list)
            {

                bool reversed = false;

                while (rest_day.count_2 < 7 && cooks_morn_2.Count > 0)
                {
                    rest_day.cooks.Add(cooks_morn_2.ElementAt(0));
                    rest_day.count_2 += cooks_morn_2.ElementAt(0).hours;
                    cooks_morn_2.RemoveAt(0);
                    
                }

                if (!reversed && cooks_even_2.Count != 0 && rest_day.count_2 >= 7)
                {
                    cooks_even_2 = cooks_even_2.OrderBy(cook => cook.hours).ToList();
                    reversed = true;
                }
                
                if (cooks_morn_2.Count == 0)
                {
                    if (reversed)
                    {
                        cooks_even_2 = cooks_even_2.OrderByDescending(cook => cook.hours).ToList();
                        reversed = false;
                    }

                    while (rest_day.count_2 < 7 && cooks_even_2.Count > 0 && (cooks_even_2.ElementAt(0).hours + rest_day.count_2) < 14)
                    {
                        rest_day.cooks.Add(cooks_even_2.ElementAt(0));
                        rest_day.count_2 += cooks_even_2.ElementAt(0).hours;
                        cooks_even_2.RemoveAt(0);
                    }
                }

                while (rest_day.count_2 < 14 && cooks_even_2.Count > 0)
                {
                    rest_day.cooks.Add(cooks_even_2.ElementAt(0));
                    rest_day.count_2 += cooks_even_2.ElementAt(0).hours;
                    cooks_even_2.RemoveAt(0);

                }

                if (reversed)
                {
                    cooks_even_2 = cooks_even_2.OrderByDescending(cook => cook.hours).ToList();
                    reversed = false;
                }

            }

            j = 1;
            while (j < count)
            {

                for (int i = j; i < j + 2; i++)
                {
                    if (i >= count)
                    {
                        break;
                    }
                    if (i != 1)
                    {
                        for (int k = 0; k < days.ElementAt(1).list.Count; k++)
                        {
                            days.ElementAt(i).list.Add(new RestaurantDay(days.ElementAt(1).list.ElementAt(k)));
                        }
                        days.ElementAt(i).list = (days.ElementAt(1).list);
                    }

                }

                j += 4;
            }


            //Filling the schedule by cooks, 2/2, from 4 day
            
            foreach (var rest_day in days.ElementAt(3).list)
            {
                bool reversed = false;

                while (rest_day.count_3 < 7 && cooks_morn_2.Count > 0)
                {
                    rest_day.cooks.Add(cooks_morn_2.ElementAt(0));
                    rest_day.count_3 += cooks_morn_2.ElementAt(0).hours;
                    cooks_morn_2.RemoveAt(0);
                }

                if (!reversed && cooks_even_2.Count != 0 && rest_day.count_3 >= 7)
                {
                    cooks_even_2 = cooks_even_2.OrderBy(cook => cook.hours).ToList();
                    reversed = true;
                }

                if (cooks_morn_2.Count == 0)
                {
                    while (rest_day.count_3 < 7 && cooks_even_2.Count > 0 && (cooks_even_2.ElementAt(0).hours + rest_day.count_3) < 14)
                    {
                        rest_day.cooks.Add(cooks_even_2.ElementAt(0));
                        rest_day.count_3 += cooks_even_2.ElementAt(0).hours;
                        cooks_even_2.RemoveAt(0);
                    }
                }

                while (rest_day.count_3 < 14 && cooks_even_2.Count > 0)
                {
                    rest_day.cooks.Add(cooks_even_2.ElementAt(0));
                    rest_day.count_3 += cooks_even_2.ElementAt(0).hours;
                    cooks_even_2.RemoveAt(0);

                }

                if (reversed)
                {
                    cooks_even_2 = cooks_even_2.OrderByDescending(cook => cook.hours).ToList();
                    reversed = false;
                }

            }

            j = 3;
            while (j < count)
            {

                for (int i = j; i < j + 2; i++)
                {
                    if (i >= count)
                    {
                        break;
                    }
                    if (i != 3)
                    {
                        for (int k = 0; k < days[3].list.Count; k++)
                        {
                            days[i].list[k] = (new RestaurantDay(days[3].list[k]));
                        }
                        days.ElementAt(i).list = (days.ElementAt(3).list);
                    }
                }

                j += 4;
            }

            return days;
        }

        public List<Schedule> GenerateSchedule(List<Cook> cooks_t, List<Qualification> qualifications, int count, int numbOfRest)
        {
            var listOfDays = Generate(cooks_t, qualifications, count);
            List<Schedule> schedule = new List<Schedule>();
            Schedule sch = new Schedule();
            for (int i = 0; i < count; i++)
            {
                var day = listOfDays[i];
                var cooks = day.list[numbOfRest].cooks.ToList();
                var cooks_morn = day.list[numbOfRest].cooks.Where(c => c.shift_type.Equals("утренняя")).OrderByDescending(t => t.hours).ToList();
                var cooks_even = day.list[numbOfRest].cooks.Where(c => c.shift_type.Equals("вечерняя")).OrderByDescending(t => t.hours).ToList();

                int morn_time = 0;
                if (cooks_morn.Count > 0)
                {   
                    if (cooks_morn.Count >= 1)
                    {
                        foreach (var item in cooks_morn)
                        {   
                            sch = new Schedule();
                            sch.numberOfDay = day.id;
                            sch.name = String.Format("{0} {1} {2}", item.surname, item.first_name, item.patronymic);
                            if (item.hours > 7)
                            {
                                sch.begin = 10;
                                sch.end = sch.begin + item.hours;
                            }
                            else
                            {
                                if (morn_time + item.hours < 7)
                                {   
                                    sch.begin = morn_time;
                                    morn_time += item.hours;
                                    sch.end = sch.begin + item.hours;
                                }
                                else
                                {
                                    if (17 - item.hours < 10)
                                    {
                                        sch.begin = 10;
                                        sch.end = 10 + item.hours;
                                    }
                                    else
                                    {
                                        sch.end = 17;
                                        sch.begin = 17 - item.hours;
                                    }
                                }
                            }
                            schedule.Add(sch);
                        }   
                    }
                    
                }


                int even_time = 0;
                if (cooks_even.Count > 0)
                {
                    if (cooks_even.Count >= 1)
                    {
                        foreach (var item in cooks_even)
                        {
                            sch = new Schedule();
                            sch.numberOfDay = day.id;
                            sch.name = String.Format("{0} {1} {2}", item.surname, item.first_name, item.patronymic);
                            if (item.hours > 7)
                            {
                                sch.end = 24;
                                sch.begin = sch.end - item.hours;
                            }
                            else
                            {
                                if (even_time + item.hours < 7)
                                {
                                    sch.end = 24 - even_time;
                                    even_time += item.hours; 
                                    sch.begin = sch.end - item.hours;
                                }
                                else
                                {
                                    sch.end = 24;
                                    sch.begin = 24 - item.hours;
                                }
                            }
                            schedule.Add(sch);

                        }
                    }

                }
            }

            return schedule;
        }
        
    }


  
}







/*  foreach (var day in days)
 {
    var rus = cooks_morn_5.FindAll(cook => cook.qualifications.Contains(qualifications.ElementAt(0))).ToList();
     cooks_morn_5.RemoveAll(cook => cook.qualifications.Contains(qualifications.ElementAt(0)));
     var jap = cooks_morn_5.FindAll(cook => cook.qualifications.Contains(qualifications.ElementAt(1))).ToList();
     cooks_morn_5.RemoveAll((cook => cook.qualifications.Contains(qualifications.ElementAt(1))));
     var ital = cooks_morn_5.FindAll(cook => cook.qualifications.Contains(qualifications.ElementAt(2))).ToList();

     foreach (var rest_day in day.list)
     {
         if (rest_day.count_rus < 8)
         {
		               
            if (rus.Count > 0)
            {
                rest_day.cooks.Add(rus.ElementAt(0));
                rest_day.count_rus += rus.ElementAt(0).hours;
                rus.RemoveAt(0);
            }
         }

         if (rest_day.count_jap < 8)
         {
             rest_day.cooks.Add(jap.ElementAt(0));
             rest_day.count_jap += jap.ElementAt(0).hours;
             jap.RemoveAt(0);
         }

         if (rest_day.count_ital < 8)
         {
                rest_day.cooks.Add(ital.ElementAt(0));
                rest_day.count_ital += ital.ElementAt(0).hours;
                ital.RemoveAt(0);
         }


     }
* */