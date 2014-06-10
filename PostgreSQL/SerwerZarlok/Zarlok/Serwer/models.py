from __future__ import unicode_literals

from django.db import models

class Achievement(models.Model):
    name = models.CharField(max_length=100, blank=True)
	
    class Meta:
        db_table = 'Achievement'

class Category(models.Model):
    name = models.CharField(max_length=100, blank=True)
	
    class Meta:
        db_table = 'Category'
		
    def __unicode__(self):
        return self.name

class Eaten(models.Model):
    foodid = models.ForeignKey('Food', db_column='foodId', blank=True, null=True) # Field name made lowercase.
    profileid = models.ForeignKey('Profile', db_column='profileId', blank=True, null=True) # Field name made lowercase.
	
    date = models.DateTimeField(blank=True, null=True)
    class Meta:
        db_table = 'Eaten'
    
    def __unicode__(self):
        return self.foodid.name + " [" + self.profileid.login + "]"

class Food(models.Model):
    name = models.CharField(max_length=100, blank=True)
    calories = models.IntegerField(blank=True, null=True)
    categoryid = models.ForeignKey(Category, db_column='categoryId', blank=True, null=True) # Field name made lowercase.

    def __unicode__(self):
        return self.name
	
    class Meta:
        db_table = 'Food'

class Friendship(models.Model):
    sender = models.ForeignKey('Profile', db_column='sender', blank=True, null=True, related_name="senders")
    receiver = models.ForeignKey('Profile', db_column='receiver', blank=True, null=True, related_name="receivers")
	
    class Meta:
        db_table = 'Friendship'

class Profile(models.Model):
    login = models.CharField(max_length=20)
    password = models.CharField(max_length=50)
    firstname = models.CharField(max_length=100, blank=True)
    surname = models.CharField(max_length=100, blank=True)
    type = models.CharField(max_length=50,blank=True, null=True)
	
    class Meta:
        db_table = 'Profile'

class Profileachievement(models.Model):
    profileid = models.ForeignKey(Profile, db_column='profileId', blank=True, null=True) # Field name made lowercase.
    achievementid = models.ForeignKey(Achievement, db_column='achievementId', blank=True, null=True) # Field name made lowercase.
    date = models.DateTimeField(blank=True, null=True)
	
    class Meta:
        db_table = 'ProfileAchievement'

class Request(models.Model):
    sender = models.ForeignKey(Profile, db_column='sender', blank=True, null=True, related_name="senders_request")
    receiver = models.ForeignKey(Profile, db_column='receiver', blank=True, null=True, related_name="receivers_request")
    sent = models.DateTimeField(blank=True, null=True)
	
    class Meta:
        db_table = 'Request'
		
class Identifier(models.Model):
	keyIdentifier = models.CharField(max_length=100)
	profileid = models.ForeignKey(Profile, db_column='profileId', blank=True, null=True)
	date = models.DateTimeField(blank=True, null=True)
    
	class Meta:
		db_table = 'Identifier'