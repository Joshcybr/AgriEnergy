
        $(document).ready(function () {
            // When Edit button is clicked
            $(".btn-edit").click(function () {
                var row = $(this).closest("tr");

                // Get current values
                var name = row.find(".product-name").text();
                var price = row.find(".product-price").text().replace('₽', '').trim(); // Remove currency sign
                var description = row.find(".product-description").text();

                // Change to input fields
                row.find(".product-name").html('<input type="text" value="' + name + '" class="form-control" />');
                row.find(".product-price").html('<input type="text" value="' + price + '" class="form-control" />');
                row.find(".product-description").html('<input type="text" value="' + description + '" class="form-control" />');

                // Change buttons to save and cancel
                row.find(".btn-edit").replaceWith('<button class="btn btn-success btn-save" data-id="' + $(this).data("id") + '">Save</button>');
                row.find(".btn-delete").replaceWith('<button class="btn btn-secondary btn-cancel">Cancel</button>');
            });

        // When Save button is clicked
        $(document).on("click", ".btn-save", function () {
                var row = $(this).closest("tr");
        var productId = $(this).data("id");

        var updatedName = row.find(".product-name input").val();
        var updatedPrice = row.find(".product-price input").val();
        var updatedDescription = row.find(".product-description input").val();

        // Send updated data to server using AJAX
        $.ajax({
            url: '@Url.Action("Update", "Product")',
        type: 'POST',
        data: {
            id: productId,
        name: updatedName,
        price: updatedPrice,
        description: updatedDescription
                    },
        success: function (response) {
                        if (response.success) {
            // Update table with new data
            row.find(".product-name").html(updatedName);
        row.find(".product-price").html('₽' + updatedPrice); // Add currency symbol
        row.find(".product-description").html(updatedDescription);

        // Change buttons back to Edit and Delete
        row.find(".btn-save").replaceWith('<button class="btn btn-warning btn-edit" data-id="' + productId + '">Edit</button>');
        row.find(".btn-cancel").replaceWith('<button class="btn btn-danger btn-delete" data-id="' + productId + '">Delete</button>');
                        } else {
            alert("Error updating product.");
                        }
                    }
                });
            });

        // When Cancel button is clicked
        $(document).on("click", ".btn-cancel", function () {
                var row = $(this).closest("tr");

        // Revert to previous values
        row.find(".product-name").html(row.find(".product-name input").val());
        row.find(".product-price").html(row.find(".product-price input").val());
        row.find(".product-description").html(row.find(".product-description input").val());

        // Change buttons back to Edit and Delete
        row.find(".btn-save").replaceWith('<button class="btn btn-warning btn-edit" data-id="' + $(this).data(" id") + '">Edit</button>');
    row.find(".btn-cancel").replaceWith('<button class="btn btn-danger btn-delete" data-id="' + $(this).data("id") + '">Delete</button>');
});

// When Delete button is clicked
$(document).on("click", ".btn-delete", function () {
    var row = $(this).closest("tr");
    var productId = $(this).data("id");

    if (confirm("Are you sure you want to delete this product?")) {
        $.ajax({
            url: '@Url.Action("Delete", "Product")',
            type: 'POST',
            data: { id: productId },
            success: function (response) {
                if (response.success) {
                    row.remove();
                } else {
                    alert("Error deleting product.");
                }
            }
        });
    }
});
        });
