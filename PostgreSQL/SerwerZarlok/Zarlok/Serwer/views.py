from django.shortcuts import render
from django.http import HttpResponse

import uuid
import base64
from datetime import datetime

from models import *
import json

# ---------- GET ----------------


def checkIdentifier(id):
	identifier = Identifier.objects.filter(keyIdentifier=id)
	if len(identifier)==0:
		return None
	else:
		return identifier[0].profileid

def login(request, login, password):
	profile = Profile.objects.filter(login=login, password=password)
	if len(profile)>0:
		profile = profile[0] # First profile
	
		val = base64.b64encode(uuid.uuid4().bytes).replace('=', '')
		id = Identifier.objects.filter(profileid=profile)
		
		if len(id)==0:
			i = Identifier()
		else:
			i = id[0]
			
		i.profileid=profile
		i.date = datetime.now()
		i.keyIdentifier = val
		i.save()
		
		return HttpResponse(val)
	else:
		return HttpResponse("Error")

		
def categories(request, id):
	if checkIdentifier(id) is not None:
		categories = Category.objects.all()
		
		list = []
		for c in categories:
			list.append({ 'id' : c.id, 'name' : c.name })
		
		return HttpResponse( json.dumps( { 'categories' : list, }) )
	else:
		return HttpResponse("Error")

def food(request, id):
	if checkIdentifier(id) is not None:
		food = Food.objects.all()
		
		list = []
		for f in food:
			list.append({ 'id': f.id, 'name' : f.name, 'calories' : f.calories, 'category' : f.categoryid.id })
		
		return HttpResponse( json.dumps( { 'food' : list, }) )
	else:
		return HttpResponse("Error")

		
def history(request, id):
	user = checkIdentifier(id)

	if user is not None: 
		hist = Eaten.objects.filter(profileid = user).order_by('-date')[:20]
		
		ach = Profileachievement.objects.filter(profileid = user).order_by('date')[:3]
		
		list = []
		for f in hist:
			list.append({ 'id': f.foodid.id, 'date' : f.date.isoformat() })
		
		achiev = []
		for a in ach:
			achiev.append({'achievement' : a.achievementid.name, 'date' : a.date.isoformat() })
		
		return HttpResponse( json.dumps( { 'history' : list, 'achievements' : achiev }) )
	else:
		return HttpResponse("Error")
	
def achievements(request, id):
	user = checkIdentifier(id)

	if user is not None: 
		hist = Profileachievement.objects.filter(profileid = user).order_by('date')[:20]
		
		list = []
		for f in hist:
			list.append({ 'id': f.id, 'date' : f.date.isoformat() })
		
		return HttpResponse( json.dumps( { 'achievements' : list, }) )
	else:
		return HttpResponse("Error")

# -------- SET ---------------
def eaten(request, id, foodid):
	user = checkIdentifier(id)

	if user is not None: 
		e = Eaten()
		e.foodid = Food.objects.get(id=foodid)
		e.date = datetime.now()
		e.profileid = user
		e.save()
		
		return HttpResponse("Success")
	else:
		return HttpResponse("Error")
