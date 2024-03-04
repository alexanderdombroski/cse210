using System;

class Program
{
    static void Main(string[] args) {
        Job job1 = new() {
            _company = "Apple",
            _jobTitle = "Software Engineer",
            _startYear = 2018,
            _endYear = 2020
        };
        Job job2 = new() {
            _company = "Microsoft",
            _jobTitle = "Project Manager",
            _startYear = 2020,
            _endYear = 2022
        };
        
        Resume michael = new() {
            _name = "Michael Smith"
        };
        michael._jobs.Add(job1);
        michael._jobs.Add(job2);

        michael.Display();
    }
}