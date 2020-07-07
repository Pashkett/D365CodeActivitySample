# D365CodeActivitySample

## Description
### Primary Entity:
 - Contact;
### Input:
 - Parameters:
    - Two search attributes as schema name (strings);
    - Two search values (strings);
### Return:
#### Int Status Result:
  - Contacts count is equal to 0:
      - Int Status Result set as 2;
  - Contacts count is equal to 1:
      - Int Status Result set as 1;
  
  - Contacts count is more than 1:
      - Int Status Result set as 3;
#### Contact Reference Result:
  - Contacts count is equal to 1:
      - Contact Reference Result set as Contact EntityReferece
