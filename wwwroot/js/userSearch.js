// userSearch.js

document.addEventListener("DOMContentLoaded", function () {
    const searchField = document.getElementById("searchField");
    const suggestionsContainer = document.getElementById("userSearchResults");

    searchField.addEventListener("input", async function () {
        const searchTerm = searchField.value;

        if (searchTerm.length >= 2) {
            const response = await fetch(`/Home/SearchUsers?searchTerm=${searchTerm}`);
            const suggestions = await response.json();

            suggestionsContainer.innerHTML = ""; // Clear the previous suggestions

            if (suggestions && suggestions.length > 0) {
                for (let suggestion of suggestions) {
                    const suggestionDiv = document.createElement("div");
                    suggestionDiv.innerHTML = `<a href="/Home/Profile/${suggestion.id}">${suggestion.fullName}</a>`;
                    suggestionsContainer.appendChild(suggestionDiv);
                }
                suggestionsContainer.style.display = "block"; // Show the suggestions container
            } else {
                suggestionsContainer.style.display = "none"; // Hide the suggestions container if no results
            }
        } else {
            suggestionsContainer.style.display = "none"; // Hide the suggestions container if search term is too short
        }
    });
});
