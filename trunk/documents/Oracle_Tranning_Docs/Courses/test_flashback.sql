
--SYS--------
/*
grant flashback any table to HR;
*/
--HR--------------
/*

alter table hr.employees enable row movement;
alter table hr.jobs enable row movement;
alter table hr.departments enable row movement;

select * from hr.employees where employee_id=198;
update hr.employees set first_name='Chung test' where employee_id=198; 
commit;
*/

