#!/usr/bin/env python


#Test : Sarah Oppenheimer"

import re
import math
import sys
import argparse
import numpy as np
from sklearn import preprocessing

def main(location):

	out = []
	
	# Open OSM file
	with open("map-2.osm") as fp:
	
		# Read all lines between node delimeters
         	for result in re.findall('<node (.*?) </node>', fp.read(), re.S):
         	
         		# Only focus on results that contains a tag
         		if "tag" in result:
         		
         			# Limit the length of result to less than 1000. This is an arbirary value
         			if len(result) < 1000:
         			
         				if location in result:
         				
         					for split in result.split() :
         					
         						if "lat" in split or "lon" in split:
         						
         							temp = re.findall(r"[-+]?\d*\.\d+|\d+", split)
         							
         							out.append( temp[0] )
         				
	return out
	
	
def haversine(coord1, coord2):
    	R = 6372800  # Earth radius in meters
    	lat1, lon1 = coord1
    	lat2, lon2 = coord2
    	
    	lat1  = float(lat1)
    	lon1 = float(lon1)
    	lat2  = float(lat2)
    	lon2 = float(lon2)
    
    	phi1, phi2 = math.radians(lat1), math.radians(lat2) 
    	
    	dphi       = math.radians(lat2 - lat1)
    	
    	dlambda    = math.radians(lon2 - lon1)
    
    	a = math.sin(dphi/2)**2 + math.cos(phi1)*math.cos(phi2)*math.sin(dlambda/2)**2
    
    	return 2*R*math.atan2(math.sqrt(a), math.sqrt(1 - a))
    	
def pointvector(coord1, coord2):

    	lat1, lon1 = coord1
    	lat2, lon2 = coord2
    	
    	lat1  = float(lat1)
    	lon1 = float(lon1)
    	lat2  = float(lat2)
    	lon2 = float(lon2)
    	
    	pv = ((lat2 - lat1) * 10000), ((lon2 - lon1) * 10000)
    	
    	u = np.array([pv[0], pv[1]])
    	
    	print(unit_vector(u))
    	
    	
def unit_vector(v):
    	return v / np.linalg.norm(v)

    	
if __name__ == '__main__':

	parser = argparse.ArgumentParser(description='A tutorial of argparse!')
	
	parser.add_argument("--a", default=1, help="This is the 'a' variable")
	
	parser.add_argument("--name", required=True, type=str, help="Target Landmark Name")
	
	parser.add_argument("--origin", required=False, type=str, help="Origin Location coordinates")
			
	parser.add_argument("--point_vector", required=False, type=str, help="Origin Location coordinates")
	
	args = parser.parse_args()
	
	a = args.name
	
	ret = main(a)
	
	ret2 = args.origin.split(',')
	
	print( "Origin: " + " lat: " + ret2[0] +  " lon: "+ ret2[1])

	print( "Target: " + " lat: " + ret[0] +  " lon: "+ ret[1])
	
	myradians = math.atan2( float(ret[0])  - float(ret2[0]), float(ret[1])  - float(ret2[1] ) )
		
	print(math.degrees(myradians))
	print(haversine(ret2,ret))
	
	pointvector(ret2,ret)
	
	
	
	

