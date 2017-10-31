Feature: Tests
	In order to get through the Online test
	As a automation test engineer
	I need to clear the below mentioned three tests

@ci
Scenario: 01.Verify that the Valtech homepage displays the latest news section
	Given I launch the valtech website
	Then I should see the latest news displayed on the page

#This testcase is to demonstrate the scenario outline feature only.
Scenario Outline: 02. Verify the correponding tab menu pages reflects the respective page title
  Given I launch the valtech website
  When I navigate to the tab menu "<tabMenuName>"
  Then I should see the page title as "<tabMenuTitle>"
Examples: 
| tabMenuName | tabMenuTitle |
| About       | About        |
| Services    | Services     |
| Work        | Work         |

Scenario: 03. Verify that user should be able to navigate to Contact page and calculate the number of offices
	Given I launch the valtech website
	When I navigate to the tab menu "Contacts" page
	Then I should be able to calculate the number of offices


