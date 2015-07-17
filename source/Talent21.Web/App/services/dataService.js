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
        return $http.get(v + 'candidate/paged');
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
        return $http.post(v + 'job/search', query);
    }

    factory.contractor.jobById = function (id) {
        return $http.get(v + 'candidate/job/' + id);
    }

    factory.contractor.ApplyToJob = function (id) {
        return $http.post(v + 'candidate/job/' + id + '/apply');
    }

    factory.contractor.ShortlistJobApplication = function (id) {
        return $http.put(v + 'candidate/job/' + id + '/favorite');
    }

    factory.userid = "" ;


    factory.contractor.createSchedule = function (formData) {
        return $http.post(v + 'candidate/schedule', formData);
    }

    factory.contractor.editSchedule = function (formData) {
        return $http.put(v + 'candidate/schedule', formData);
    }
    factory.contractor.deleteSchedule = function (formData) {
        return $http.delete(v + 'candidate/schedule', formData);
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

    factory.company.myJobs = function () {
        return $http.get(v + 'company/job/all');
    }

    factory.company.limitJobs = function () {
        return $http.get(v + 'company/job/paged');
    }

    factory.job.publish = function (record) {
        return $http.put( v + 'company/job/publish', record);
    }

    factory.job.cancel = function (record) {
        return $http.put(v + 'company/job/cancel', record);
    }

    factory.job.delete = function (record) {
        return $http.delete(v + 'company/job', record);
    }
    
    factory.company.search = function (query) {
        return $http.post(v + 'company/search', query);
    }   
    //For Jobs

    factory.job.profile = function (id) {
        return $http.get(v + 'company/job/ ' + id);
    }

    factory.job.newjob = function (formData) {
        return $http.post(v + 'company/job', formData);
    }

    factory.job.editjob = function (record) {
        return $http.put(v + 'company/job', record);
    }

    //System requests

    factory.system.getLocations = function () {
        return $http.get(v + 'system/location/all');
    }

    factory.system.getSkills = function () {
        return $http.get(v + 'system/skill/all');
    }

    factory.system.getIndustries = function () {
        return $http.get(v + 'system/industry/all');
    }

    return factory;
}]);