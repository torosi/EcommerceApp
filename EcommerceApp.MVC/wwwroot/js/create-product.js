
const productTypeDropdownId = 'product-types-drop-down';
var loadedProductTypes = false;

function init(getProductTypeUrl) {
    initProductTypeDropDown(getProductTypeUrl);
}

function initProductTypeDropDown(getProductTypeUrl) {
    // Get the drop down for product types
    const dropdown = document.getElementById('product-types-drop-down');
    if (dropdown == undefined || dropdown == null) {
        throw new Error("Failed to load product type drop down");
    }

    // Add event listeners to load drop down data
    dropdown.addEventListener('click', function () {
        if (!loadedProductTypes) {
            loadDropDownData(getProductTypeUrl, productTypeDropdownId);
            loadedProductTypes = true;
        }
    });
}

function loadDropDownData(url, dropdownId, optionId) {
    const dropdown = $(`#${dropdownId}`);
    if (dropdown.length === 0) {
        throw new Error(`Drop down was not found.`);
    }

    $.ajax({
        url: url,
        type: 'GET',
        success: function (response) {
            // Check if the data contains results
            if (response.data && response.data.length > 0) {
                // Iterate through each results
                $.each(response.data, function (index, entity) {
                    dropdown.append($('<option>', {
                        value: entity.id,
                        text: entity.name,
                        selected: entity.id === optionId
                    }));
                });
            } else {
                // Show a message if there are no results
                dropdown.append('<option disabled>No results available</option>');
            }
        },
        error: function (ex) {
            console.error("Error loading results.");
        }
    });
}