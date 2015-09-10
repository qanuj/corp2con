app.controller('adminIndustryController', ['$scope', 'dataService', '$routeParams','$rootScope', function ($scope, db, params,$rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        Metronic.initAjax();
    });

    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;

    $scope.title = "Industry";
    var master = db.system.industry;

    $scope.navigate = function (page) {
        page = page || $scope.currentPage || 1;
        master.paged(page).success(function (result) {
            $scope.currentPage = page;
            $scope.pages = Math.ceil(result.count / db.pageSize);
            $scope.records = result.items;
        });
    }

    $scope.save = function (record) {
        $('input[type=text]').each(function () {
            $(this).val('');
        });
        master.add(record).success($scope.navigate);
    }

    $scope.update = function (record) {
        master.update(record).success($scope.navigate);
    }

    $scope.delete = function (record) {
        master.remove(record).success($scope.navigate);
    }

    $scope.toggle = function (record) {
        record.editMode = !record.editMode;
    }

    $scope.navigate(params.page);
}]);