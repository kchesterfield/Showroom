# Showroom

Showcase of code and demonstrations of principles. To run a demonstration, select a project and then run. For now its best to run in debug mode.

------

## Showroom.LispValidator

A simple checker that validates the parentheses of a LISP code.  Takes in a string as an input and returns true if all the parentheses in the string are properly closed and nested.

Run the project from Visual Studio and then navigate to the swagger page. Swagger pages can be accessed via `/swagger`

Once in Swagger, open one of the following APIs. Select `Try it out` which will unlock the UI elements for editing.

In the fields you may enter either a valid JSON string or a Base64 encoded string for testing. Selecting `Execute` will create the API call and the LISP code will be validated.

------

## Showroom.User

A simple user registration that will then add users to a JSON file after every submission.

**Note** The JSON file gets overwritten after every deployment. To review the contents navigate to: `\Showroom\Showroom.User\bin\Debug\netcoreapp3.1\Data` 

Run the project from Visual Studio and then navigate to `/user.html` where you be given a form to submit users.

------

## Showroom.Enrollment

Reviews a CSV document and transform the data for another application to consume. For demonstration sake, this site has been setup to be have like an API and not a full front end experience.

Run the project from Visual Studio and then navigate to the Swagger page. Swagger pages can be accessed via `/swagger`

Enter either a Base64 encoded string of the CSV document, or you may use a CSV in a JSON compliant string format.

**Note** Sample data has been provided and can be found under the `TestData` folder.

**Note** Currently the CSV must not contain any quotation marks and not contain a header. The mapping of fields has been restricted to: UserId, FirstAndLastName, Version, InsuranceCompany

------

## Showroom.SQL

A simple demonstration of SQL scripts. No database has been currently provided to test these scripts on.



