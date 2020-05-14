angular.module("umbraco")
    .controller("My.ColorsDropdownList", function ($scope, $routeParams, mediaHelper, mediaResource, $location, listViewHelper, $http) {
        $scope.custom = $scope.model.value;
        $http({
            url: "/admin/backoffice/api/colorsdropdownlist/getallcolors",
            method: "GET"
        }).then(function (ent) {
            $scope.items = ent.data;
            });
        var scope = $scope;
        $scope.$on("formSubmitting", function (ev, args) {
            scope.model.value = $("#color-dropdown-picker").val();
        });
    });