# Car-dealership-C-

## This is one of my projects from the second semester of university.

## Classes

### Vehicle class

#### Fields

1. Licence plate
- String
- It cannot be modified outside the class
- Can't be null or empty string
- Rules: Hungarian old Licence plate (example: ABC-123)
- The last three number can't be "000"

2. Construction Year
- Integer 
- It cannot be modified outside the class
- The value must be between 1950 and 2024
- Can be declared only once
- When the value is greater than 2024, it will be changed to the current year

3. Original price
- Integer
- It cannot be modified outside the class
- The value must be between 300000 and 12000000
- Can be declaired only once
- Classic property methods

4. Condition
- Enum
- Values: "Like a new", "Well kept", "Damaged", "Defective"

5. Age
- Integer
- Only readable property
- Difference between the current year and the construction year

6. Extra price
- Integer
- Only readable property
- When the vehicle is a maximum two years old and its conditition is "Like a new", then the extra price is 2% of the original price
- Late binding.

#### Constructors

1. Constructor with every datas(Licence plate, Cunstruction year, Original price, Condition)

2. Constructor with every datas, except the Condition. It will be automatically "Well kept" by invoking the previous constructor 

#### Methods

1. PurchasePrice()
- The result is integer
- Late binding
- Scheme: Round(OriginalPrice * Power(amortization, Age) + ExtraPrice)

| Condition | Amortisation |
| --------- | ------------ |
| Like a new | 9% |
| Well kept | 10% |
| Damaged | 11% |
| Defective | 12% |

2. ToString() Overriding
- Datas from the vehicle will be outwritten

3. Equals() Overriding
- Two vehicles are equal when they have the same Licence plate

### Car class

#### Fields

##### This class will be the child class to the Vehicle class

1. Passenger number
- Integer
- It cannot be modified outside the class
- The value can only be 2, 4, 5 or 7

2. Tow hitch
- Bool

3. Air condition
- Enum
- Values: "None", "Manual", "Digital", "Digital with more zone"

4. Extra price (Override)
- Integer
- Only readable property
- When the car is a maximum two years old and its conditition is "Like a new", then the extra price is 2% of the original price
- If the car equipped with tow hitch, an additional +60000 Huf applies
- If the passenger number is "7", an additional +100000 Huf applies
- Table with additional price for the air conditioning:

| Air condition | Price |
| ------------- | ----- |
| None | 0 Huf |
| Manual | 40000 Huf |
| Digital | 150000 Huf |
| Digital with more zone | 350000 Huf | 

#### Constructors

1. Constructor with every datas(Licence plate, Cunstruction year, Original price, Condition, Passenger number, Air condition)

2. Constructor with every datas, except the Condition and the Air condition. It will be automatically "Well kept" and "Digital" by invoking the previous constructor

#### Methods

1. PurchasePrice() (Override)

- Scheme is the same
- Values:

| Condition | Amortisation |
| --------- | ------------ |
| Like a new | 8% |
| Well kept | 9% |
| Damaged | 12% |
| Defective | 13% |

- If the Passenger number is "7", then the Amortisation is 1.2 times the original rate

2. ToString() (Override)

- Datas from the car will be outwritten



