Feature: Add customer
Background: User will be on Homepage

@E2E-Deposit_Amount
Scenario: Add customer and Open Account
	#Given User will be on Homepage
	When User will click on the Manager Login Button
	Then Manager Page is loaded in the same page
	When User Clicks on the Add customer Select Button
	* Fills the Customer Details '<firstname>','<lastname>','<postcode>'
	* Clicks the Add Customer Button
	Then Alert appears with Message
	When Clicks the Open Account select Button
	* Selects the customer name,'<firstname>','<lastname>' and '<currency>'
	* Clicks the Process Button
	Then Alert appears with a Message
Examples:
	| firstname | lastname | postcode | currency |
	| John      | Doe      | 680306   | Rupee    |



