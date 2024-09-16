select t2.FIRSTNAME + ' ' + t2.LASTNAME as "NAME", t1.NUMBER, t1.TYPE
from PHONE t1
	join PERSON t2
	on t2.ID = t1.PERSONID
order by t2.ID;

select t2.FIRSTNAME + ' ' + t2.LASTNAME as "PERSON",
	t3.FIRSTNAME + ' ' + t3.LASTNAME as "RELATED PERSON",
	t1.TYPE
from PERSONALRELATIONS t1
	join PERSON t2
	on t2.ID = t1.PERSONID
	join PERSON t3
	on t3.ID = t1.RELATEDPERSONID
order by t2.ID;
