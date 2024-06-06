CREATE TABLE PersonDetails (
   Id INTEGER PRIMARY KEY AUTOINCREMENT,
   BirthDay DATE,
   PersonCity TEXT,
   PersonId INTEGER,
   FOREIGN KEY (PersonId)
   REFERENCES Persons(PersonId)  
);