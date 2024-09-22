

function loadCategories(url, dropdownSelector, categoryId) {
    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            var categoryList = $(dropdownSelector);
            categoryList.empty(); // Clear the dropdown list

            // Check if the data contains categories
            if (response.data && response.data.length > 0) {
                // Iterate through each category
                $.each(response.data, function (index, category) {
                    categoryList.append($('<option>', {
                        value: category.id,
                        text: category.name,
                        selected: category.id === categoryId 
                    }));
                    console.log(">>> " + category.id)
                    console.log(">>> " + categoryId)
                    console.log(">>> " + category.id === categoryId)
                });
            } else {
                // Show a message if there are no categories
                categoryList.append('<option disabled>No categories available</option>');
            }
        },
        error: function (ex) {
            console.error("Failed to load categories.");
            $(dropdownSelector).empty().append('<option disabled>Error loading categories</option>');
        }
    });
}