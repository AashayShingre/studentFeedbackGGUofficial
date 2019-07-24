#create database
create database feedbackmanagement;
use feedbackmanagement;
#create school table
create table schools (school_id int auto_increment primary key, school_name varchar(100) not null);
#create departments table
create table departments (dept_id int auto_increment primary key, dept_name varchar(100) not null, school_id int not null, foreign key (school_id) references schools(school_id));
#create courses table
create table courses (course_id int auto_increment primary key, course_name varchar(100) not null, dept_id int not null, foreign key (dept_id) references departments(dept_id));
#create teachers table
create table teachers (teacher_id int auto_increment primary key, teacher_name varchar(100) not null, course_id int not null, foreign key (course_id) references courses(course_id));
#create subjects table
create table subjects (subject_id int auto_increment primary key, subject_name varchar(100) not null, course_id int not null, foreign key (course_id) references courses(course_id));
#create feedback table (make sure to make last two columns unique) 
create table feedbacks ( prop1_vg int default 0, prop1_g int default 0, prop1_s int default 0, prop1_b int default 0,
                               prop2_vg int default 0, prop2_g int default 0, prop2_s int default 0, prop2_b int default 0,
                               prop3_vg int default 0, prop3_g int default 0, prop3_s int default 0, prop3_b int default 0,
                                prop4_vg int default 0, prop4_g int default 0, prop4_s int default 0, prop4_b int default 0,
                                prop5_vg int default 0, prop5_g int default 0, prop5_s int default 0, prop5_b int default 0,
                                prop6_vg int default 0, prop6_g int default 0, prop6_s int default 0, prop6_b int default 0,
                                prop7_vg int default 0, prop7_g int default 0, prop7_s int default 0, prop7_b int default 0,
                                prop8_vg int default 0, prop8_g int default 0, prop8_s int default 0, prop8_b int default 0,
                                prop9_vg int default 0, prop9_g int default 0, prop9_s int default 0, prop9_b int default 0,
                                prop10_vg int default 0, prop10_g int default 0, prop10_s int default 0, prop10_b int default 0,
                                prop11_vg int default 0, prop11_g int default 0, prop11_s int default 0, prop11_b int default 0, teacher_id int, subject_id int, unique (teacher_id, subject_id));