# Car-dealership-C-

## This is one of my projects from the second semester of university.

## Classes

### Vehicle

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
- When the vehicle is a maximum two years old and its conditition is "Like a new", then the extra price is 2% of the original price. 
- Late binding.

#### Constructors

1. Constructor with every datas(Licence plate, Cunstruction year, Original price, Condition).

2. constructor with every datas, except the Condition. It will be automatically "Well kept" by invoking the previous constructor. 

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
	