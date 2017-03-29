using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cis237inclass5
{
    class Program
    {
        private string username = "ksherman";
        private string password = "password";


        static void Main(string[] args)
        {
            CarsKShermanEntities CarTestEntities = new CarsKShermanEntities();
            Console.WriteLine("Print the list");
            foreach (Car car in CarTestEntities.Cars)
            {
                Console.WriteLine(car.id + " " + car.make + " " + car.model);
            }


            // Handles reading
            //*************************************
            // Find a specific one by any property
            //*************************************

            // call the where method on the table cars and pass in a bamda expression
            // for the criteria we are looking for. There is nothing special about the 
            // word car in the part that reads: car => car.id == "v0...". It could be any
            // characters we want it to be.
            // it is just a variable name for the current car we are considering 
            // in the expression. this will automatically loop through all 
            // the cars,
            // and run the expression against each of them. When the result is finally 
            // true, it will return that car

            Console.WriteLine(Environment.NewLine + "=============== FIND BY ID ===============");
            Car carTofind = CarTestEntities.Cars.Where(
            car => car.id == "V0LCD1814").First();

            Console.WriteLine(carTofind.id + " " + carTofind.make + " " + carTofind.model);

            // we can look for a specific model from the database with a where based 
            // on any crieria we want. Here is one the is looking to match the car's model instead of the id.
            Car otherCarToFind = CarTestEntities.Cars.Where(
                foocar => foocar.model == "Challenger").First();

            Console.WriteLine(Environment.NewLine + "=============== FIND BY Model ===============");
            Console.WriteLine(otherCarToFind.id + " " + otherCarToFind.make + " " + otherCarToFind.model);

            // we can also find one based on the primary key
            // since it must be unique, we will only return one
            // so the where isn't needed
            // the .find method expects that you are finding by Primary ID
            // will return null if it can't find anything
            Car foundCar = CarTestEntities.Cars.Find("V0LCD1814");
            Console.WriteLine(Environment.NewLine + "=============== FIND BY ID ===============");
            if(foundCar == null)
                Console.WriteLine("That model doesn't exist");
            else
                Console.WriteLine(foundCar.id + " " + foundCar.make + " " + foundCar.model);


            // Handles writing
            //************************************
            // Add a new car to the database
            //************************************
            Car newCarToAdd = new Car();

            // assign properties to the parts of the model
            newCarToAdd.id = "2017";
            newCarToAdd.make = "Nissan";
            newCarToAdd.model = "Skyline";
            newCarToAdd.horsepower = 550;
            newCarToAdd.cylinders = 8;
            newCarToAdd.year = "2017";
            newCarToAdd.type = "car";

            Console.WriteLine(Environment.NewLine + "=============== ADD BY ID ===============");
            // us a try catch to ensure that they can't add a car with an existing ID
            try
            {
                // add the new car to the cars collection
                // done locally, so we must push it up after it checks
                CarTestEntities.Cars.Add(newCarToAdd);

                // this method call actually does the work of saving te changes to the database
                CarTestEntities.SaveChanges();
                Console.WriteLine("done.");
            }
            catch(Exception e)
            {
                // remove the new car from the cars collection since we cant save it
                CarTestEntities.Cars.Remove(newCarToAdd);
                Console.WriteLine("Can't add the record. Already have one with that primary key");
            }
            Console.WriteLine("Just added a new car. Gonig to fetch it and print it to verify");
            carTofind = CarTestEntities.Cars.Find(newCarToAdd.id);
            Console.WriteLine(carTofind.id + " " + carTofind.make + " " + carTofind.model);

            // Handles deleting
            //************************************
            // delete a car from the database
            //************************************


            Console.WriteLine(Environment.NewLine + "=============== REMOVE BY ID ===============");
            // get a car out of the database that we want to delete
            Car carToFindForDelete = CarTestEntities.Cars.Find("2017");

            // remove the car fron the cars collection
            CarTestEntities.Cars.Remove(carToFindForDelete);

            CarTestEntities.SaveChanges();
            Console.WriteLine("Just removed a car. Gonig to fetch it and print it to verify");
            carTofind = CarTestEntities.Cars.Find(carToFindForDelete.id);
            if (carTofind == null)
                Console.WriteLine("That car doesn't exist");
            else
                Console.WriteLine(carTofind.id + " " + carTofind.make + " " + carTofind.model);


            // Handles Updating
            //************************************
            // update a car in the database
            //************************************

            // get a car out of the database that we would like to update
            Car carToFindForUpdate = CarTestEntities.Cars.Find("V0LCD1814");

            Console.WriteLine("done.");

            Console.WriteLine(Environment.NewLine + "=============== UPDATE BY ID ===============");
            // output the car to find
            Console.WriteLine(Environment.NewLine + Environment.NewLine + "About to do an update");
            Console.WriteLine(carToFindForUpdate.id + " " + carToFindForUpdate.make +
                                    " " + carToFindForUpdate.model);
            Console.WriteLine("Doing the update now");

            // update some of the properties of the car we found
            // all of them if we don't want to
            carToFindForUpdate.make = "Dodge";
            carToFindForUpdate.model = "Ram 1500";
            carToFindForUpdate.horsepower = 5000;
            carToFindForUpdate.cylinders = 8;

            // save the new updates to the database. Since when we pulled out the one 
            // to update, all we were really doing was getting a reference to the one in
            // the collection we wanted to update, there is no need ot 'put' the car
            // back into the cars collection. it is still there.
            // all we have to do is save the changes.
            CarTestEntities.SaveChanges();

            Console.WriteLine(Environment.NewLine + Environment.NewLine + "showing the car after update");
            Console.WriteLine(carToFindForUpdate.id + " " + carToFindForUpdate.make +
                                    " " + carToFindForUpdate.model + " " + carToFindForUpdate.horsepower + " " + carToFindForUpdate.cylinders);
        }
    }
}