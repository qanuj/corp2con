app.factory('dataService', ['$http', function ($http) {
    var v = 'api/v1/';
    var factory = {
        contractor: {},
        job: {},
        company: {},
        system: {}
    };

    factory.role = document.querySelector('html').dataset.role;

    factory.currentPage = window.location.href;

    //For Contractors

    factory.contractor.paged = function () {
        return $http.get(v + 'candidate/paged?$inlinecount=allpages');
    }

    factory.contractor.all = function () {
        return $http.get(v + 'candidate/all');
    }

    factory.contractor.profile = function () {
        return $http.get(v + 'candidate/profile');
    }

    factory.contractor.editProfile = function (formData) {
        return $http.put(v + 'candidate/profile', formData);
    }

    factory.contractor.searchJob = function (query) {
        return $http.post(v + 'job/search',query);
    }

    factory.contractor.createSchedule = function (formData) {
        return $http.post(v + 'candidate/schedule', formData);
    }
    factory.contractor.getSchedule = function () {
        return $http.get(v + 'candidate/schedule/all');
    }

    
    //For companies

    factory.company.paged = function () {
        return $http.get(v + 'company/paged?$inlinecount=allpages');
    }

    factory.company.all = function () {
        return $http.get(v + 'company/all');
    }

    factory.company.profile = function () {
        return $http.get(v + 'company/profile');
    }

    factory.company.newjob = function (formData) {
        return $http.put(v + 'company/job', formData);
    }

    factory.company.editProfile = function (formData) {
        return $http.put(v + 'company/profile', formData);
    }

    factory.company.myJobs = function() {
        return $http.get(v + 'company/job/all');
    }

    //For Jobs

    factory.job.profile = function (id) {
        return $http.get(v + 'company/job/ ' + id);
    }

    factory.job.newjob = function (formData) {
        return $http.post(v + 'company/job', formData);
    }

    return factory;
}]);