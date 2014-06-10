# This is an auto-generated Django model module.
# You'll have to do the following manually to clean this up:
#   * Rearrange models' order
#   * Make sure each model has one field with primary_key=True
#   * Remove `managed = False` lines for those models you wish to give write DB access
# Feel free to rename the models, but don't rename db_table values or field names.
#
# Also note: You'll have to insert the output of 'django-admin.py sqlcustom [appname]'
# into your database.
from __future__ import unicode_literals

from django.db import models

class Achievement(models.Model):
    name = models.CharField(max_length=100, blank=True)
    class Meta:
        managed = False
        db_table = 'Achievement'

class Category(models.Model):
    name = models.CharField(max_length=100, blank=True)
    class Meta:
        managed = False
        db_table = 'Category'

class Eaten(models.Model):
    foodid = models.ForeignKey('Food', db_column='foodId', blank=True, null=True) # Field name made lowercase.
    profileid = models.ForeignKey('Profile', db_column='profileId', blank=True, null=True) # Field name made lowercase.
    date = models.DateTimeField(blank=True, null=True)
    class Meta:
        managed = False
        db_table = 'Eaten'

class Food(models.Model):
    name = models.CharField(max_length=100, blank=True)
    calories = models.IntegerField(blank=True, null=True)
    categoryid = models.ForeignKey(Category, db_column='categoryId', blank=True, null=True) # Field name made lowercase.
    class Meta:
        managed = False
        db_table = 'Food'

class Friendship(models.Model):
    sender = models.ForeignKey('Profile', db_column='sender', blank=True, null=True)
    receiver = models.ForeignKey('Profile', db_column='receiver', blank=True, null=True)
    class Meta:
        managed = False
        db_table = 'Friendship'

class Profile(models.Model):
    login = models.CharField(max_length=20)
    password = models.CharField(max_length=50)
    firstname = models.CharField(max_length=100, blank=True)
    surname = models.CharField(max_length=100, blank=True)
    type = models.SmallIntegerField(blank=True, null=True)
    class Meta:
        managed = False
        db_table = 'Profile'

class Profileachievement(models.Model):
    profileid = models.ForeignKey(Profile, db_column='profileId', blank=True, null=True) # Field name made lowercase.
    date = models.DateTimeField(blank=True, null=True)
    class Meta:
        managed = False
        db_table = 'ProfileAchievement'

class Request(models.Model):
    sender = models.ForeignKey(Profile, db_column='sender', blank=True, null=True)
    receiver = models.ForeignKey(Profile, db_column='receiver', blank=True, null=True)
    sent = models.DateTimeField(blank=True, null=True)
    class Meta:
        managed = False
        db_table = 'Request'

