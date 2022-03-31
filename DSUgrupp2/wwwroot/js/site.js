let map;
let markers = [];
let lat;
let lng;
let CurrentLat;
let CurrentLng;
let marker


if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(showPosition);
    
} else {
    x.innerHTML = "Geolocation is not supported by this browser.";
}

function showPosition(position) {
    var x = document.getElementById("test");
    x.innerHTML = "Latitude: " + position.coords.latitude +
        "<br>Longitude: " + position.coords.longitude;
    CurrentLat = position.coords.latitude;
    CurrentLng = position.coords.longitude;


}


// Adds a marker to the map and push to the array.
function addMarker(position) {
    deleteMarkers()
    const marker = new google.maps.Marker({
        position,
        map
    });
    
    markers.push(marker);
    CurrentLat = marker.getPosition().lat();
    CurrentLng = marker.getPosition().lng();
    document.getElementById("test").innerHTML = (`Här är dina koordinater ${CurrentLat}, ${CurrentLng}`)
}


function submitForm() {

    $(document).ready(function () {
        $("#btnSearch").click(function () {
            $.ajax({
                url: '/weather/DetailCoordinates',
                type: 'POST',
                dataType: 'json',
                data: { lat: CurrentLat, lng: CurrentLng, date: '2022-01-27 14:00:00' },
                success: function (data) {

                }, error: function (data) {

                }
            });
        });
    });
}


// This example adds a search box to a map, using the Google Place Autocomplete
// feature. People can enter geographical searches. The search box will return a
// pick list containing a mix of places and predicted search terms.
// This example requires the Places library. Include the libraries=places
// parameter when you first load the API. For example:
// <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg&libraries=places">
function initAutocomplete() {

    if (CurrentLat == null) {
        map = new google.maps.Map(document.getElementById("map"), {
            zoom: 12,
            center: { lat: 63.1904073, lng: 14.659041 },
            mapTypeId: "terrain",

        });
        addMarker({ lat: 63.1904073, lng: 14.659041 });
    }
    
    else {
        map = new google.maps.Map(document.getElementById("map"), {
            zoom: 12,
            center: { lat: CurrentLat, lng: CurrentLng },
            mapTypeId: "terrain",
            
        });

        addMarker({ lat: CurrentLat, lng: CurrentLng })
    }
    map.addListener("click", (event) => {
        addMarker(event.latLng);
        
    });
    // Create the search box and link it to the UI element.
    const input = document.getElementById("pac-input");
    const searchBox = new google.maps.places.SearchBox(input);

    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    // Bias the SearchBox results towards current map's viewport.
    map.addListener("bounds_changed", () => {
        searchBox.setBounds(map.getBounds());
    });

    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener("places_changed", () => {
  
        const places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach((marker) => {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        const bounds = new google.maps.LatLngBounds();

        places.forEach((place) => {
            if (!place.geometry || !place.geometry.location) {
                console.log("Returned place contains no geometry");
                return;
            }

            const icon = {
                
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25),
            };

            // Create a marker for each place.
            markers.push(
                new google.maps.Marker({
                    map,
                    icon,
                    title: place.name,
                    position: place.geometry.location,
                })
                
            );
            
            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
               

            } else {
                bounds.extend(place.geometry.location);
            }

            var x = document.getElementById("test");
            x.innerHTML = "Latitude: " + + place.geometry.viewport.Ab.g + "<br>Longitude: " + place.geometry.viewport.Ra.g;
            CurrentLat = place.geometry.viewport.Ab.g
            CurrentLng = place.geometry.viewport.Ra.g;
        });

        

        map.fitBounds(bounds);
        
    });
}


// This event listener will call addMarker() when the map is clicked.

// add event listeners for the buttons
document
    .getElementById("show-markers")
    .addEventListener("click", showMarkers);
document
    .getElementById("hide-markers")
    .addEventListener("click", hideMarkers);
document
    .getElementById("delete-markers")
    .addEventListener("click", deleteMarkers);
// Adds a marker at the center of the map.


// Sets the map on all markers in the array.
function setMapOnAll(map) {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
       
        
    }
}

// Removes the markers from the map, but keeps them in the array.
function hideMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    hideMarkers();
    markers = [];
    marker = null;
}

function showMenu() {

    if (document.getElementById('popupDiv').style.display === 'none') {
        document.getElementById('popupDiv').style.display = 'block';
    }
    else {
    hideMenu()
    }
}

function hideMenu() {
    document.getElementById('popupDiv').style.display = 'none';

}