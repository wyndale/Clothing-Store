// Clear Authorization Header on Logout
function clearAuthHeader() {
    const ui = window.ui;
    if (ui) {
        ui.authActions.logout(); // This will clear the token in Swagger UI
    }
}

// Execute the function to clear the token
clearAuthHeader();
