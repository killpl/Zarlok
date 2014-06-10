from django.conf.urls import patterns, include, url
from django.contrib import admin

from Serwer.views import *

admin.autodiscover()

urlpatterns = patterns('',
    # Examples:
    # url(r'^$', 'Zarlok.views.home', name='home'),
    # url(r'^blog/', include('blog.urls')),

    url(r'^admin/', include(admin.site.urls)),
	url(r'^login/(.*)/(.*)/', login),
	url(r'^categories/(.*)/', categories),
	url(r'^food/(.*)/', food),
	url(r'^history/(.*)/', history),
	url(r'^achievements/(.*)/', achievements),
	url(r'^eat/(.*)/(.*)/', eaten),
	
)
