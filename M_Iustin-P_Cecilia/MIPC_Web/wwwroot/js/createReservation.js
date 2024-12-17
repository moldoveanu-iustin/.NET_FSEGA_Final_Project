let map;
let startAutocomplete, stopAutocomplete;
let startPoint, stopPoint;
//const pricePerKm = @Model.PretPerKm; // Get the price per km from the server
const pricePerKm = parseFloat(document.getElementById('pricePerKm').value);
function initAutocomplete() {
    // Initialize Google Places Autocomplete
    console.log("Google Maps initialized");
    startAutocomplete = new google.maps.places.Autocomplete(document.getElementById('startLocation'));
    console.log("Start autocomplete initialized:", startAutocomplete);
    stopAutocomplete = new google.maps.places.Autocomplete(document.getElementById('stopLocation'));
    console.log("Stop autocomplete initialized:", stopAutocomplete);

    // Add event listeners for place selection
    startAutocomplete.addListener('place_changed', () => {
        const place = startAutocomplete.getPlace();
        if (place.geometry) {
            startPoint = place.geometry.location;
            calculateDistanceAndPrice();
        }
    });

    stopAutocomplete.addListener('place_changed', () => {
        const place = stopAutocomplete.getPlace();
        if (place.geometry) {
            stopPoint = place.geometry.location;
            calculateDistanceAndPrice();
        }
    });
}

function calculateDistanceAndPrice() {
    if (startPoint && stopPoint) {
        // Calculate distance in kilometers
        const distanceInMeters = google.maps.geometry.spherical.computeDistanceBetween(startPoint, stopPoint);
        const distanceInKm = (distanceInMeters / 1000).toFixed(2);

        // Update distance and price fields
        document.getElementById('totalDistance').value = distanceInKm;
        document.getElementById('price').value = (distanceInKm * pricePerKm).toFixed(2);
    }
}

// Initialize the autocomplete on page load
//google.maps.event.addDomListener(window, 'load', initAutocomplete);
window.addEventListener('load', initAutocomplete);