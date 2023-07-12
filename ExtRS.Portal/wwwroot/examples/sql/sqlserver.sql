--
--
-- SQL Server tables used to run the Editor examples.
--
-- For more information about how the client and server-sides interact, please
-- refer to the Editor documentation: http://editor.datatables.net/manual .
--
-- Please note that SQL Server 2008 or newer is required for this script. Also,
-- if you plan to use server-side processing with DataTables, SQL Server 2012 or
-- newer is required.
--
--

--
-- To do list examples
--
IF OBJECT_ID('todo', 'U') IS NOT NULL
  DROP TABLE todo;

CREATE TABLE todo (
    id int not null identity,
    item nvarchar(255) NOT NULL default '',
    done bit NOT NULL default '0',
    priority integer NOT NULL default 1,
    PRIMARY KEY (id)
);

INSERT INTO todo (item, done, priority)
    VALUES
        ( 'Send business plan to clients', 1, 1 ),
        ( 'Web-site copy revisions',       0, 2 ),
        ( 'Review client tracking',        0, 2 ),
        ( 'E-mail catchup',                0, 3 ),
        ( 'Complete worksheet',            0, 4 ),
        ( 'Prep sales presentation',       0, 5 );



--
-- Users table examples
--
IF OBJECT_ID('user_dept', 'U') IS NOT NULL
  DROP TABLE user_dept;
IF OBJECT_ID('user_permission', 'U') IS NOT NULL
  DROP TABLE user_permission;
IF OBJECT_ID('user_access', 'U') IS NOT NULL
  DROP TABLE user_access; -- legacy
IF OBJECT_ID('users_files', 'U') IS NOT NULL
  DROP TABLE users_files;
IF OBJECT_ID('users_visits', 'U') IS NOT NULL
  DROP TABLE users_visits;
  
IF OBJECT_ID('dept', 'U') IS NOT NULL
  DROP TABLE dept;
IF OBJECT_ID('permission', 'U') IS NOT NULL
  DROP TABLE permission;
IF OBJECT_ID('sites', 'U') IS NOT NULL
  DROP TABLE sites;
IF OBJECT_ID('users', 'U') IS NOT NULL
  DROP TABLE users;
IF OBJECT_ID('files', 'U') IS NOT NULL
  DROP TABLE files;

CREATE TABLE users (
    id int not null identity,
    title nvarchar(255) default NULL,
    first_name nvarchar(255) default NULL,
    last_name nvarchar(255) default NULL,
    phone nvarchar(100) default NULL,
    city nvarchar(50) default NULL,
    zip nvarchar(10) default NULL,
    updated_date datetime DEFAULT GETDATE(),
    registered_date datetime,
    removed_date datetime,
    active bit default NULL,
    comments nvarchar(255) default NULL,
    manager int default NULL,
    site int default NULL,
    image int default NULL,
    shift_start time default NULL,
    shift_end time default NULL,
    description text default NULL,
    PRIMARY KEY (id)
); 

GO

DROP TRIGGER IF EXISTS update_users_timestamp;
GO
CREATE TRIGGER update_users_timestamp ON users
FOR UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT UPDATE (updated_date)
    BEGIN
        UPDATE al
        SET updated_date = GETDATE()
        FROM users AS al
        INNER JOIN inserted AS i
            ON al.id = i.id;
    END
END

GO


CREATE TABLE dept (
    id int not null identity, 
    name nvarchar(250) default NULL,
    PRIMARY KEY (id)
);

CREATE TABLE permission (
    id int not null identity,  
    name nvarchar(250) default NULL,
    PRIMARY KEY (id)
);

CREATE TABLE sites (
    id int not null identity,  
    name nvarchar(250) default NULL,
    continent nvarchar(250) default NULL,
    PRIMARY KEY (id)
);

CREATE TABLE files (
    id int not null identity, 
    filename nvarchar(250) default NULL,
    filesize int default 0,
    web_path nvarchar(250) default NULL,
    system_path nvarchar(250) default NULL,
    PRIMARY KEY (id)
);


-- Expect only one dept per user
CREATE TABLE user_dept (
    user_id int,
    dept_id int,
    PRIMARY KEY (user_id, dept_id),
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (dept_id) REFERENCES dept(id) ON DELETE CASCADE
);

-- Expect multiple permission per user
CREATE TABLE user_permission (
    user_id int,
    permission_id int,
    PRIMARY KEY (user_id, permission_id),
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (permission_id) REFERENCES permission(id) ON DELETE CASCADE
);

CREATE TABLE users_files (
    user_id int NOT NULL,
    file_id int NOT NULL,
    FOREIGN KEY (user_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (file_id) REFERENCES files(id) ON DELETE CASCADE
);
GO


CREATE OR ALTER VIEW staff_newyork as
  select id, first_name, last_name, phone, city
  from users
  where site in (select id from sites where name = 'New York');
GO

INSERT INTO users (title, first_name, last_name, phone, city, zip, registered_date, active, manager, site, shift_start, shift_end)
    VALUES
        ('Miss','Quynn',     'Contreras',    '1-971-977-4681', 'Slidell',               '81080',    '2012-04-06T18:53:00', '0', 1, 1, '08:00:00', '16:00:00'),
        ('Mr',  'Kaitlin',   'Smith',        '1-436-523-6103', 'Orlando',               'U5G 7J3',  '2012-11-20T05:58:25', '1', 1, 2, '09:00:00', '17:00:00'),
        ('Mrs', 'Cruz',      'Reynolds',     '1-776-102-6352', 'Lynn',                  'EJ89 9DQ', '2011-12-31T23:34:03', '0', 2, 3, '09:00:00', '17:00:00'),
        ('Dr',  'Sophia',    'Morris',       '1-463-224-1405', 'Belleville',            'T1F 2X1',  '2012-08-04T02:55:53', '0', 3, 4, '08:00:00', '15:30:00'),
        ('Miss','Kamal',     'Roberson',     '1-134-408-5227', 'Rehoboth Beach',        'V7I 6T5',  '2012-12-23T00:17:03', '1', 1, 5, '09:00:00', '17:00:00'),
        ('Dr',  'Dustin',    'Rosa',         '1-875-919-3188', 'Jersey City',           'E4 8ZE',   '2012-10-05T22:18:59', '0', 1, 6, '09:00:00', '17:00:00'),
        ('Dr',  'Xantha',    'George',       '1-106-884-4754', 'Billings',              'Y2I 6J7',  '2012-11-25T12:50:16', '0', 6, 1, '07:00:00', '15:00:00'),
        ('Mrs', 'Bryar',     'Long',         '1-918-114-8083', 'San Bernardino',        '82983',    '2012-05-14T23:32:25', '0', 1, 2, '09:00:00', '17:00:00'),
        ('Mrs', 'Kuame',     'Wynn',         '1-101-692-4039', 'Truth or Consequences', '21290',    '2011-06-21T16:27:07', '1', 2, 3, '06:00:00', '14:00:00'),
        ('Ms',  'Indigo',    'Brennan',      '1-756-756-8161', 'Moline',                'NO8 3UY',  '2011-02-19T12:51:08', '1', 5, 4, '12:00:00', '00:00:00'),
        ('Mrs', 'Avram',     'Allison',      '1-751-507-2640', 'Rancho Palos Verdes',   'I7Q 8H4',  '2012-12-30T17:02:10', '0', 1, 5, '09:00:00', '17:00:00'),
        ('Mr',  'Martha',    'Burgess',      '1-971-722-1203', 'Toledo',                'Q5R 9HI',  '2011-02-04T17:25:55', '1', 1, 6, '12:00:00', '00:00:00'),
        ('Miss','Lael',      'Kim',          '1-626-697-2194', 'Lake Charles',          '34209',    '2012-07-24T06:44:22', '1', 7, 1, '09:00:00', '17:00:00'),
        ('Dr',  'Lyle',      'Lewis',        '1-231-793-3520', 'Simi Valley',           'H9B 2H4',  '2012-08-30T03:28:54', '0', 1, 2, '00:00:00', '12:00:00'),
        ('Miss','Veronica',  'Marks',        '1-750-981-6759', 'Glens Falls',           'E3C 5D1',  '2012-08-14T12:09:24', '1', 2, 3, '09:00:00', '17:00:00'),
        ('Mrs', 'Wynne',     'Ruiz',         '1-983-744-5362', 'Branson',               'L9E 6E2',  '2012-11-06T01:04:07', '0', 1, 4, '12:00:00', '00:00:00'),
        ('Ms',  'Jessica',   'Bryan',        '1-949-932-6772', 'Boulder City',          'F5P 6NU',  '2013-02-01T20:22:33', '0', 5, 5, '09:00:00', '17:00:00'),
        ('Ms',  'Quinlan',   'Hyde',         '1-625-664-6072', 'Sheridan',              'Y8A 1LQ',  '2011-10-25T16:53:45', '1', 1, 6, '08:00:00', '15:00:00'),
        ('Miss','Mona',      'Terry',        '1-443-179-7343', 'Juneau',                'G62 1OF',  '2012-01-15T09:26:59', '0', 1, 1, '08:30:00', '16:30:00'),
        ('Mrs', 'Medge',     'Patterson',    '1-636-979-0497', 'Texarkana',             'I5U 6E0',  '2012-10-20T16:26:18', '1', 1, 2, '09:00:00', '17:00:00'),
        ('Mrs', 'Perry',     'Gamble',       '1-440-976-9560', 'Arcadia',               '98923',    '2012-06-06T02:03:49', '1', 2, 3, '00:00:00', '12:00:00'),
        ('Mrs', 'Pandora',   'Armstrong',    '1-197-431-4390', 'Glendora',              '34124',    '2011-08-29T01:45:06', '0', 7, 4, '21:00:00', '03:00:00'),
        ('Mr',  'Pandora',   'Briggs',       '1-278-288-9221', 'Oneida',                'T9M 4H9',  '2012-07-16T08:44:41', '1', 4, 5, '09:00:00', '17:00:00'),
        ('Mrs', 'Maris',     'Leblanc',      '1-936-114-2921', 'Cohoes',                'V1H 6Z7',  '2011-05-04T13:07:04', '1', 1, 6, '00:00:00', '12:00:00'),
        ('Mrs', 'Ishmael',   'Crosby',       '1-307-243-2684', 'Midwest City',          'T6 8PS',   '2011-07-02T23:11:11', '0', 3, 1, '09:00:00', '17:00:00'),
        ('Miss','Quintessa', 'Pickett',      '1-801-122-7471', 'North Tonawanda',       '09166',    '2013-02-05T10:33:22', '1', 1, 2, '12:00:00', '00:00:00'),
        ('Miss','Ifeoma',    'Mays',         '1-103-883-0962', 'Parkersburg',           '87377',    '2011-08-22T12:19:09', '0', 1, 3, '09:00:00', '17:00:00'),
        ('Mrs', 'Basia',     'Harrell',      '1-528-238-4178', 'Cody',                  'LJ54 1IU', '2012-05-07T14:42:55', '1', 1, 4, '09:00:00', '17:00:00'),
        ('Mrs', 'Hamilton',  'Blackburn',    '1-676-857-1423', 'Delta Junction',        'X5 9HE',   '2011-05-19T07:39:48', '0', 6, 5, '10:00:00', '18:00:00'),
        ('Ms',  'Dexter',    'Burton',       '1-275-332-8186', 'Gainesville',           '65914',    '2013-02-01T16:21:20', '1', 5, 6, '21:00:00', '03:00:00'),
        ('Mrs', 'Quinn',     'Mccall',       '1-808-916-4497', 'Fallon',                'X4 8UB',   '2012-03-24T19:31:51', '0', 1, 1, '09:00:00', '17:00:00'),
        ('Mr',  'Alexa',     'Wilder',       '1-727-307-1997', 'Johnson City',          '16765',    '2011-10-14T08:21:14', '0', 3, 2, '09:00:00', '17:00:00'),
        ('Ms',  'Rhonda',    'Harrell',      '1-934-906-6474', 'Minnetonka',            'I2R 1H2',  '2011-11-15T00:08:02', '1', 1, 3, '12:00:00', '00:00:00'),
        ('Mrs', 'Jocelyn',   'England',      '1-826-860-7773', 'Chico',                 '71102',    '2012-05-31T18:01:43', '1', 1, 4, '09:00:00', '17:00:00'),
        ('Dr',  'Vincent',   'Banks',        '1-225-418-0941', 'Palo Alto',             '03281',    '2011-08-07T07:22:43', '0', 1, 5, '18:00:00', '02:00:00'),
        ('Mrs', 'Stewart',   'Chan',         '1-781-793-2340', 'Grand Forks',           'L1U 3ED',  '2012-11-01T23:14:44', '1', 6, 6, '08:00:00', '16:00:00');

INSERT INTO dept (name)
    VALUES
        ( 'IT' ),
        ( 'Sales' ),
        ( 'Pre-Sales' ),
        ( 'Marketing' ),
        ( 'Senior Management' ),
        ( 'Accounts' ),
        ( 'Support' );

INSERT INTO permission (name)
    VALUES
        ( 'Printer' ),
        ( 'Servers' ),
        ( 'Desktop' ),
        ( 'VMs' ),
        ( 'Web-site' ),
        ( 'Accounts' );


INSERT INTO user_dept (user_id, dept_id)
    VALUES
        ( 1,  1 ),
        ( 2,  4 ),
        ( 3,  7 ),
        ( 4,  3 ),
        ( 5,  2 ),
        ( 6,  6 ),
        ( 7,  2 ),
        ( 8,  1 ),
        ( 9,  2 ),
        ( 10, 3 ),
        ( 11, 4 ),
        ( 12, 5 ),
        ( 13, 6 ),
        ( 14, 4 ),
        ( 15, 3 ),
        ( 16, 6 ),
        ( 17, 3 ),
        ( 18, 7 ),
        ( 19, 7 ),
        ( 20, 1 ),
        ( 21, 2 ),
        ( 22, 6 ),
        ( 23, 3 ),
        ( 24, 4 ),
        ( 25, 5 ),
        ( 26, 6 ),
        ( 27, 7 ),
        ( 28, 2 ),
        ( 29, 3 ),
        ( 30, 1 ),
        ( 31, 3 ),
        ( 32, 4 ),
        ( 33, 6 ),
        ( 34, 7 ),
        ( 35, 2 ),
        ( 36, 3 );


INSERT INTO user_permission (user_id, permission_id)
    VALUES
        ( 1,  1 ),
        ( 1,  3 ),
        ( 1,  4 ),
        ( 2,  4 ),
        ( 2,  1 ),
        ( 4,  3 ),
        ( 4,  4 ),
        ( 4,  5 ),
        ( 4,  6 ),
        ( 5,  2 ),
        ( 6,  6 ),
        ( 7,  2 ),
        ( 8,  1 ),
        ( 9,  2 ),
        ( 10, 3 ),
        ( 10, 2 ),
        ( 10, 1 ),
        ( 11, 4 ),
        ( 11, 6 ),
        ( 12, 5 ),
        ( 12, 1 ),
        ( 12, 2 ),
        ( 13, 1 ),
        ( 13, 2 ),
        ( 13, 3 ),
        ( 13, 6 ),
        ( 18, 3 ),
        ( 18, 2 ),
        ( 18, 1 ),
        ( 20, 1 ),
        ( 20, 2 ),
        ( 20, 3 ),
        ( 21, 2 ),
        ( 21, 4 ),
        ( 22, 6 ),
        ( 22, 3 ),
        ( 22, 2 ),
        ( 30, 1 ),
        ( 30, 5 ),
        ( 30, 3 ),
        ( 31, 3 ),
        ( 32, 4 ),
        ( 33, 6 ),
        ( 34, 1 ),
        ( 34, 2 ),
        ( 34, 3 ),
        ( 35, 2 ),
        ( 36, 3 );

INSERT INTO sites (name, continent)
    VALUES
        ( 'Edinburgh', 'Europe' ),
        ( 'London', 'Europe' ),
        ( 'Paris', 'Europe' ),
        ( 'New York', 'North America' ),
        ( 'Singapore', 'Asia' ),
        ( 'Los Angeles', 'North America' );


--
-- Cascading lists examples
--
IF OBJECT_ID('team', 'U') IS NOT NULL
  DROP TABLE team;

IF OBJECT_ID('country', 'U') IS NOT NULL
  DROP TABLE country;

IF OBJECT_ID('continent', 'U') IS NOT NULL
  DROP TABLE continent;


CREATE TABLE continent (
    id int not null identity,
    name nvarchar(250) default NULL,
    PRIMARY KEY (id)
);

INSERT INTO continent (name)
    VALUES
		('Africa'),
		('Asia'),
		('Europe'),
		('N. America'),
		('Oceania'),
		('S. America');


CREATE TABLE country (
    id int not null identity,
    name nvarchar(250) default NULL,
    continent int,
    FOREIGN KEY (continent) REFERENCES continent(id) ON DELETE CASCADE,
    PRIMARY KEY (id)
);

INSERT INTO country (name, continent)
    VALUES
		( 'Algeria', 1 ),
		( 'Angola', 1 ),
		( 'Benin', 1 ),
		( 'Botswana', 1 ),
		( 'Burkina', 1 ),
		( 'Burundi', 1 ),
		( 'Cameroon', 1 ),
		( 'Cape Verde', 1 ),
		( 'Central African Republic', 1 ),
		( 'Chad', 1 ),
		( 'Comoros', 1 ),
		( 'Congo', 1 ),
		( 'Congo, Democratic Republic of', 1 ),
		( 'Djibouti', 1 ),
		( 'Egypt', 1 ),
		( 'Equatorial Guinea', 1 ),
		( 'Eritrea', 1 ),
		( 'Ethiopia', 1 ),
		( 'Gabon', 1 ),
		( 'Gambia', 1 ),
		( 'Ghana', 1 ),
		( 'Guinea', 1 ),
		( 'Guinea-Bissau', 1 ),
		( 'Ivory Coast', 1 ),
		( 'Kenya', 1 ),
		( 'Lesotho', 1 ),
		( 'Liberia', 1 ),
		( 'Libya', 1 ),
		( 'Madagascar', 1 ),
		( 'Malawi', 1 ),
		( 'Mali', 1 ),
		( 'Mauritania', 1 ),
		( 'Mauritius', 1 ),
		( 'Morocco', 1 ),
		( 'Mozambique', 1 ),
		( 'Namibia', 1 ),
		( 'Niger', 1 ),
		( 'Nigeria', 1 ),
		( 'Rwanda', 1 ),
		( 'Sao Tome and Principe', 1 ),
		( 'Senegal', 1 ),
		( 'Seychelles', 1 ),
		( 'Sierra Leone', 1 ),
		( 'Somalia', 1 ),
		( 'South Africa', 1 ),
		( 'South Sudan', 1 ),
		( 'Sudan', 1 ),
		( 'Swaziland', 1 ),
		( 'Tanzania', 1 ),
		( 'Togo', 1 ),
		( 'Tunisia', 1 ),
		( 'Uganda', 1 ),
		( 'Zambia', 1 ),
		( 'Zimbabwe', 1 ),
		( 'Afghanistan', 2 ),
		( 'Bahrain', 2 ),
		( 'Bangladesh', 2 ),
		( 'Bhutan', 2 ),
		( 'Brunei', 2 ),
		( 'Burma (Myanmar)', 2 ),
		( 'Cambodia', 2 ),
		( 'China', 2 ),
		( 'East Timor', 2 ),
		( 'India', 2 ),
		( 'Indonesia', 2 ),
		( 'Iran', 2 ),
		( 'Iraq', 2 ),
		( 'Israel', 2 ),
		( 'Japan', 2 ),
		( 'Jordan', 2 ),
		( 'Kazakhstan', 2 ),
		( 'Korea, North', 2 ),
		( 'Korea, South', 2 ),
		( 'Kuwait', 2 ),
		( 'Kyrgyzstan', 2 ),
		( 'Laos', 2 ),
		( 'Lebanon', 2 ),
		( 'Malaysia', 2 ),
		( 'Maldives', 2 ),
		( 'Mongolia', 2 ),
		( 'Nepal', 2 ),
		( 'Oman', 2 ),
		( 'Pakistan', 2 ),
		( 'Philippines', 2 ),
		( 'Qatar', 2 ),
		( 'Russian Federation', 2 ),
		( 'Saudi Arabia', 2 ),
		( 'Singapore', 2 ),
		( 'Sri Lanka', 2 ),
		( 'Syria', 2 ),
		( 'Tajikistan', 2 ),
		( 'Thailand', 2 ),
		( 'Turkey', 2 ),
		( 'Turkmenistan', 2 ),
		( 'United Arab Emirates', 2 ),
		( 'Uzbekistan', 2 ),
		( 'Vietnam', 2 ),
		( 'Yemen', 2 ),
		( 'Albania', 3 ),
		( 'Andorra', 3 ),
		( 'Armenia', 3 ),
		( 'Austria', 3 ),
		( 'Azerbaijan', 3 ),
		( 'Belarus', 3 ),
		( 'Belgium', 3 ),
		( 'Bosnia and Herzegovina', 3 ),
		( 'Bulgaria', 3 ),
		( 'Croatia', 3 ),
		( 'Cyprus', 3 ),
		( 'Czech Republic', 3 ),
		( 'Denmark', 3 ),
		( 'Estonia', 3 ),
		( 'Finland', 3 ),
		( 'France', 3 ),
		( 'Georgia', 3 ),
		( 'Germany', 3 ),
		( 'Greece', 3 ),
		( 'Hungary', 3 ),
		( 'Iceland', 3 ),
		( 'Ireland', 3 ),
		( 'Italy', 3 ),
		( 'Latvia', 3 ),
		( 'Liechtenstein', 3 ),
		( 'Lithuania', 3 ),
		( 'Luxembourg', 3 ),
		( 'Macedonia', 3 ),
		( 'Malta', 3 ),
		( 'Moldova', 3 ),
		( 'Monaco', 3 ),
		( 'Montenegro', 3 ),
		( 'Netherlands', 3 ),
		( 'Norway', 3 ),
		( 'Poland', 3 ),
		( 'Portugal', 3 ),
		( 'Romania', 3 ),
		( 'San Marino', 3 ),
		( 'Serbia', 3 ),
		( 'Slovakia', 3 ),
		( 'Slovenia', 3 ),
		( 'Spain', 3 ),
		( 'Sweden', 3 ),
		( 'Switzerland', 3 ),
		( 'Ukraine', 3 ),
		( 'United Kingdom', 3 ),
		( 'Vatican City', 3 ),
		( 'Antigua and Barbuda', 4 ),
		( 'Bahamas', 4 ),
		( 'Barbados', 4 ),
		( 'Belize', 4 ),
		( 'Canada', 4 ),
		( 'Costa Rica', 4 ),
		( 'Cuba', 4 ),
		( 'Dominica', 4 ),
		( 'Dominican Republic', 4 ),
		( 'El Salvador', 4 ),
		( 'Grenada', 4 ),
		( 'Guatemala', 4 ),
		( 'Haiti', 4 ),
		( 'Honduras', 4 ),
		( 'Jamaica', 4 ),
		( 'Mexico', 4 ),
		( 'Nicaragua', 4 ),
		( 'Panama', 4 ),
		( 'Saint Kitts and Nevis', 4 ),
		( 'Saint Lucia', 4 ),
		( 'Saint Vincent and the Grenadines', 4 ),
		( 'Trinidad and Tobago', 4 ),
		( 'United States', 4 ),
		( 'Australia', 5 ),
		( 'Fiji', 5 ),
		( 'Kiribati', 5 ),
		( 'Marshall Islands', 5 ),
		( 'Micronesia', 5 ),
		( 'Nauru', 5 ),
		( 'New Zealand', 5 ),
		( 'Palau', 5 ),
		( 'Papua New Guinea', 5 ),
		( 'Samoa', 5 ),
		( 'Solomon Islands', 5 ),
		( 'Tonga', 5 ),
		( 'Tuvalu', 5 ),
		( 'Vanuatu', 5 ),
		( 'Argentina', 6 ),
		( 'Bolivia', 6 ),
		( 'Brazil', 6 ),
		( 'Chile', 6 ),
		( 'Colombia', 6 ),
		( 'Ecuador', 6 ),
		( 'Guyana', 6 ),
		( 'Paraguay', 6 ),
		( 'Peru', 6 ),
		( 'Suriname', 6 ),
		( 'Uruguay', 6 ),
		( 'Venezuela', 6 );

CREATE TABLE team (
    id int not null identity,
    name nvarchar(250) default NULL,
    country int,
    continent int,
    FOREIGN KEY (country) REFERENCES country(id) ON DELETE NO ACTION,
    FOREIGN KEY (continent) REFERENCES continent(id) ON DELETE NO ACTION,
    PRIMARY KEY (id)
);

INSERT INTO team (name, continent, country)
    VALUES
		('Caesar Vance', 4, 168 ),
		('Cara Stevens', 4, 168 ),
		('Doris Wilder', 5, 169 ),
		('Herrod Chandler', 4, 168 ),
		('Martena Mccray', 3, 144 );


--
-- Reading list example table
--
IF OBJECT_ID('audiobooks', 'U') IS NOT NULL
  DROP TABLE audiobooks;

CREATE TABLE audiobooks (
    id int not null identity,
    title nvarchar(250) NOT NULL,
    author nvarchar(250) NOT NULL,
    duration int NOT NULL,
    readingOrder int NOT NULL,
    PRIMARY KEY (id)
);

INSERT INTO audiobooks (title, author, duration, readingOrder)
    VALUES
        ( 'The Final Empire: Mistborn', 'Brandon Sanderson', 1479, 1 ),
        ( 'The Name of the Wind', 'Patrick Rothfuss', 983, 2 ),
        ( 'The Blade Itself: The First Law', 'Joe Abercrombie', 1340, 3 ),
        ( 'The Heroes', 'Joe Abercrombie', 1390, 4 ),
        ( 'Assassin''s Apprentice: The Farseer Trilogy', 'Robin Hobb', 1043, 5 ),
        ( 'The Eye of the World: Wheel of Time', 'Robert Jordan', 1802, 6 ),
        ( 'The Wise Man''s Fear', 'Patrick Rothfuss', 1211, 7 ),
        ( 'The Way of Kings: The Stormlight Archive', 'Brandon Sanderson', 2734, 8 ),
        ( 'The Lean Startup', 'Eric Ries', 523, 9 ),
        ( 'House of Suns', 'Alastair Reynolds', 1096, 10 ),
        ( 'The Lies of Locke Lamora', 'Scott Lynch', 1323, 11 ),
        ( 'Best Served Cold', 'Joe Abercrombie', 1592, 12 ),
        ( 'Thinking, Fast and Slow', 'Daniel Kahneman', 1206, 13 ),
        ( 'The Dark Tower I: The Gunslinger', 'Stephen King', 439, 14 ),
        ( 'Theft of Swords: Riyria Revelations', 'Michael J. Sullivan', 1357, 15 ),
        ( 'The Emperor''s Blades: Chronicle of the Unhewn Throne', 'Brian Staveley', 1126, 16 ),
        ( 'The Magic of Recluce: Saga of Recluce', 'L. E. Modesitt Jr.', 1153, 17 ),
        ( 'Red Country', 'Joe Abercrombie', 1196, 18 ),
        ( 'Warbreaker', 'Brandon Sanderson', 1496, 19 ),
        ( 'Magician', 'Raymond E. Feist', 2173, 20 ),
        ( 'Blood Song', 'Anthony Ryan', 1385, 21 ),
        ( 'Half a King', 'Joe Abercrombie', 565, 22 ),
        ( 'Prince of Thorns: Broken Empire', 'Mark Lawrence', 537, 23 ),
        ( 'The Immortal Prince: Tide Lords', 'Jennifer Fallon', 1164, 24 ),
        ( 'Medalon: Demon Child', 'Jennifer Fallon', 1039, 25 ),
        ( 'The Black Company: Chronicles of The Black Company', 'Glen Cook', 654, 26 );


--
-- Compound key examples
--
IF OBJECT_ID('users_visits', 'U') IS NOT NULL
  DROP TABLE audiobooks;

CREATE TABLE users_visits (
    user_id int NOT NULL,
    site_id int NOT NULL,
    visit_date datetime DEFAULT NULL,
    PRIMARY KEY (user_id, visit_date)
);


INSERT INTO users_visits (user_id, site_id, visit_date)
    VALUES
        ( 1, 1, '20160812' ),
        ( 1, 4, '20160814' ),
        ( 1, 7, '20160819' ),
        ( 2, 3, '20160712' ),
        ( 2, 2, '20160707' ),
        ( 2, 6, '20160701' ),
        ( 2, 1, '20160730' ),
        ( 3, 1, '20160626' ),
        ( 3, 2, '20161205' ),
        ( 4, 3, '20161121' ),
        ( 4, 4, '20161010' ),
        ( 5, 5, '20160802' ),
        ( 6, 6, '20160805' );

--
-- DataTables Ajax and server-side processing database (SQL Server)
--
IF OBJECT_ID('datatables_demo', 'U') IS NOT NULL
  DROP TABLE datatables_demo;

CREATE TABLE datatables_demo (
	id         int NOT NULL identity,
	first_name varchar(250) NOT NULL default '',
	last_name  varchar(250) NOT NULL default '',
	position   varchar(250) NOT NULL default '',
	email      varchar(250) NOT NULL default '',
	office     varchar(250) NOT NULL default '',
	start_date datetime default NULL,
	age        int,
	salary     int,
	seq        int,
	extn       varchar(8) NOT NULL default '',
	PRIMARY KEY (id)
);

SET IDENTITY_INSERT datatables_demo ON;

INSERT INTO datatables_demo
		( id, first_name, last_name, age, position, salary, start_date, extn, email, office, seq ) 
	VALUES
		( 1, 'Tiger', 'Nixon', 61, 'System Architect', 320800, '20110425', 5421, 't.nixon@datatables.net', 'Edinburgh', 2 ),
		( 2, 'Garrett', 'Winters', 63, 'Accountant', 170750, '20110725', 8422, 'g.winters@datatables.net', 'Tokyo', 22 ),
		( 3, 'Ashton', 'Cox', 66, 'Junior Technical Author', 86000, '20090112', 1562, 'a.cox@datatables.net', 'San Francisco', 6 ),
		( 4, 'Cedric', 'Kelly', 22, 'Senior Javascript Developer', 433060, '20120329', 6224, 'c.kelly@datatables.net', 'Edinburgh', 41 ),
		( 5, 'Airi', 'Satou', 33, 'Accountant', 162700, '20081128', 5407, 'a.satou@datatables.net', 'Tokyo', 55 ),
		( 6, 'Brielle', 'Williamson', 61, 'Integration Specialist', 372000, '20121202', 4804, 'b.williamson@datatables.net', 'New York', 21 ),
		( 7, 'Herrod', 'Chandler', 59, 'Sales Assistant', 137500, '20120806', 9608, 'h.chandler@datatables.net', 'San Francisco', 46 ),
		( 8, 'Rhona', 'Davidson', 55, 'Integration Specialist', 327900, '20101014', 6200, 'r.davidson@datatables.net', 'Tokyo', 50 ),
		( 9, 'Colleen', 'Hurst', 39, 'Javascript Developer', 205500, '20090915', 2360, 'c.hurst@datatables.net', 'San Francisco', 26 ),
		( 10, 'Sonya', 'Frost', 23, 'Software Engineer', 103600, '20081213', 1667, 's.frost@datatables.net', 'Edinburgh', 18 ),
		( 11, 'Jena', 'Gaines', 30, 'Office Manager', 90560, '20081219', 3814, 'j.gaines@datatables.net', 'London', 13 ),
		( 12, 'Quinn', 'Flynn', 22, 'Support Lead', 342000, '20130303', 9497, 'q.flynn@datatables.net', 'Edinburgh', 23 ),
		( 13, 'Charde', 'Marshall', 36, 'Regional Director', 470600, '20081016', 6741, 'c.marshall@datatables.net', 'San Francisco', 14 ),
		( 14, 'Haley', 'Kennedy', 43, 'Senior Marketing Designer', 313500, '20121218', 3597, 'h.kennedy@datatables.net', 'London', 12 ),
		( 15, 'Tatyana', 'Fitzpatrick', 19, 'Regional Director', 385750, '20100317', 1965, 't.fitzpatrick@datatables.net', 'London', 54 ),
		( 16, 'Michael', 'Silva', 66, 'Marketing Designer', 198500, '20121127', 1581, 'm.silva@datatables.net', 'London', 37 ),
		( 17, 'Paul', 'Byrd', 64, 'Chief Financial Officer (CFO)', 725000, '20100609', 3059, 'p.byrd@datatables.net', 'New York', 32 ),
		( 18, 'Gloria', 'Little', 59, 'Systems Administrator', 237500, '20090410', 1721, 'g.little@datatables.net', 'New York', 35 ),
		( 19, 'Bradley', 'Greer', 41, 'Software Engineer', 132000, '20121013', 2558, 'b.greer@datatables.net', 'London', 48 ),
		( 20, 'Dai', 'Rios', 35, 'Personnel Lead', 217500, '20120926', 2290, 'd.rios@datatables.net', 'Edinburgh', 45 ),
		( 21, 'Jenette', 'Caldwell', 30, 'Development Lead', 345000, '20110903', 1937, 'j.caldwell@datatables.net', 'New York', 17 ),
		( 22, 'Yuri', 'Berry', 40, 'Chief Marketing Officer (CMO)', 675000, '20090625', 6154, 'y.berry@datatables.net', 'New York', 57 ),
		( 23, 'Caesar', 'Vance', 21, 'Pre-Sales Support', 106450, '20111212', 8330, 'c.vance@datatables.net', 'New York', 29 ),
		( 24, 'Doris', 'Wilder', 23, 'Sales Assistant', 85600, '20100920', 3023, 'd.wilder@datatables.net', 'Sydney', 56 ),
		( 25, 'Angelica', 'Ramos', 47, 'Chief Executive Officer (CEO)', 1200000, '20091009', 5797, 'a.ramos@datatables.net', 'London', 36 ),
		( 26, 'Gavin', 'Joyce', 42, 'Developer', 92575, '20101222', 8822, 'g.joyce@datatables.net', 'Edinburgh', 5 ),
		( 27, 'Jennifer', 'Chang', 28, 'Regional Director', 357650, '20101114', 9239, 'j.chang@datatables.net', 'Singapore', 51 ),
		( 28, 'Brenden', 'Wagner', 28, 'Software Engineer', 206850, '20110607', 1314, 'b.wagner@datatables.net', 'San Francisco', 20 ),
		( 29, 'Fiona', 'Green', 48, 'Chief Operating Officer (COO)', 850000, '20100311', 2947, 'f.green@datatables.net', 'San Francisco', 7 ),
		( 30, 'Shou', 'Itou', 20, 'Regional Marketing', 163000, '20110814', 8899, 's.itou@datatables.net', 'Tokyo', 1 ),
		( 31, 'Michelle', 'House', 37, 'Integration Specialist', 95400, '20110602', 2769, 'm.house@datatables.net', 'Sydney', 39 ),
		( 32, 'Suki', 'Burks', 53, 'Developer', 114500, '20091022', 6832, 's.burks@datatables.net', 'London', 40 ),
		( 33, 'Prescott', 'Bartlett', 27, 'Technical Author', 145000, '20110507', 3606, 'p.bartlett@datatables.net', 'London', 47 ),
		( 34, 'Gavin', 'Cortez', 22, 'Team Leader', 235500, '20081026', 2860, 'g.cortez@datatables.net', 'San Francisco', 52 ),
		( 35, 'Martena', 'Mccray', 46, 'Post-Sales support', 324050, '20110309', 8240, 'm.mccray@datatables.net', 'Edinburgh', 8 ),
		( 36, 'Unity', 'Butler', 47, 'Marketing Designer', 85675, '20091209', 5384, 'u.butler@datatables.net', 'San Francisco', 24 ),
		( 37, 'Howard', 'Hatfield', 51, 'Office Manager', 164500, '20081216', 7031, 'h.hatfield@datatables.net', 'San Francisco', 38 ),
		( 38, 'Hope', 'Fuentes', 41, 'Secretary', 109850, '20100212', 6318, 'h.fuentes@datatables.net', 'San Francisco', 53 ),
		( 39, 'Vivian', 'Harrell', 62, 'Financial Controller', 452500, '20090214', 9422, 'v.harrell@datatables.net', 'San Francisco', 30 ),
		( 40, 'Timothy', 'Mooney', 37, 'Office Manager', 136200, '20081211', 7580, 't.mooney@datatables.net', 'London', 28 ),
		( 41, 'Jackson', 'Bradshaw', 65, 'Director', 645750, '20080926', 1042, 'j.bradshaw@datatables.net', 'New York', 34 ),
		( 42, 'Olivia', 'Liang', 64, 'Support Engineer', 234500, '20110203', 2120, 'o.liang@datatables.net', 'Singapore', 4 ),
		( 43, 'Bruno', 'Nash', 38, 'Software Engineer', 163500, '20110503', 6222, 'b.nash@datatables.net', 'London', 3 ),
		( 44, 'Sakura', 'Yamamoto', 37, 'Support Engineer', 139575, '20090819', 9383, 's.yamamoto@datatables.net', 'Tokyo', 31 ),
		( 45, 'Thor', 'Walton', 61, 'Developer', 98540, '20130811', 8327, 't.walton@datatables.net', 'New York', 11 ),
		( 46, 'Finn', 'Camacho', 47, 'Support Engineer', 87500, '20090707', 2927, 'f.camacho@datatables.net', 'San Francisco', 10 ),
		( 47, 'Serge', 'Baldwin', 64, 'Data Coordinator', 138575, '20120409', 8352, 's.baldwin@datatables.net', 'Singapore', 44 ),
		( 48, 'Zenaida', 'Frank', 63, 'Software Engineer', 125250, '20100104', 7439, 'z.frank@datatables.net', 'New York', 42 ),
		( 49, 'Zorita', 'Serrano', 56, 'Software Engineer', 115000, '20120601', 4389, 'z.serrano@datatables.net', 'San Francisco', 27 ),
		( 50, 'Jennifer', 'Acosta', 43, 'Junior Javascript Developer', 75650, '20130201', 3431, 'j.acosta@datatables.net', 'Edinburgh', 49 ),
		( 51, 'Cara', 'Stevens', 46, 'Sales Assistant', 145600, '20111206', 3990, 'c.stevens@datatables.net', 'New York', 15 ),
		( 52, 'Hermione', 'Butler', 47, 'Regional Director', 356250, '20110321', 1016, 'h.butler@datatables.net', 'London', 9 ),
		( 53, 'Lael', 'Greer', 21, 'Systems Administrator', 103500, '20090227', 6733, 'l.greer@datatables.net', 'London', 25 ),
		( 54, 'Jonas', 'Alexander', 30, 'Developer', 86500, '20100714', 8196, 'j.alexander@datatables.net', 'San Francisco', 33 ),
		( 55, 'Shad', 'Decker', 51, 'Regional Director', 183000, '20081113', 6373, 's.decker@datatables.net', 'Edinburgh', 43 ),
		( 56, 'Michael', 'Bruce', 29, 'Javascript Developer', 183000, '20110627', 5384, 'm.bruce@datatables.net', 'Singapore', 16 ),
		( 57, 'Donna', 'Snider', 27, 'Customer Support', 112000, '20110125', 4226, 'd.snider@datatables.net', 'New York', 19 );

	SET IDENTITY_INSERT datatables_demo OFF;