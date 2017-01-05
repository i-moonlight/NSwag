﻿/* tslint:disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v8.4.6214.23636 (NJsonSchema v7.3.6214.20986) (http://NSwag.org)
// </auto-generated>
//----------------------


export class Client {
    private baseUrl: string; 
    private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> };
    protected jsonParseReviver: (key: string, value: any) => any = undefined;

    constructor(baseUrl?: string, http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }) {
        this.baseUrl = baseUrl ? baseUrl : "";
        this.http = http ? http : window;
    }

    /**
     * Product Types
     * @latitude Latitude component of location.
     * @longitude Longitude component of location.
     * @return An array of products
     */
    products(latitude: number, longitude: number): Promise<Product[]> {
        let url_ = this.baseUrl + "/products?";
        if (latitude === undefined || latitude === null)
            throw new Error("The parameter 'latitude' must be defined and cannot be null.");
        else
            url_ += "latitude=" + encodeURIComponent("" + latitude) + "&"; 
        
        if (longitude === undefined || longitude === null)
            throw new Error("The parameter 'longitude' must be defined and cannot be null.");
        else
            url_ += "longitude=" + encodeURIComponent("" + longitude) + "&";

        return this.http.fetch(url_, {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=UTF-8", 
				"Accept": "application/json; charset=UTF-8"
            }
        }).then((response) => {
            return this.processProducts(response);
        });
    }

    protected processProducts(response: Response): Promise<Product[]> {
        return response.text().then((responseText) => {
            const status = response.status; 

            if (status === 200) {
                let result200: Product[] = null;
                let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                if (resultData200 && resultData200.constructor === Array) {
                    result200 = [];
                    for (let item of resultData200)
                        result200.push(Product.fromJS(item));
                }
                return result200;
            } else {
                let result: Error = null;
                let resultData = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                result = resultData ? Error.fromJS(resultData) : null;
                this.throwException("A server error occurred.", status, responseText, result);
            }
        });
    }

    /**
     * Price Estimates
     * @start_latitude Latitude component of start location.
     * @start_longitude Longitude component of start location.
     * @end_latitude Latitude component of end location.
     * @end_longitude Longitude component of end location.
     * @return An array of price estimates by product
     */
    price(start_latitude: number, start_longitude: number, end_latitude: number, end_longitude: number): Promise<PriceEstimate[]> {
        let url_ = this.baseUrl + "/estimates/price?";
        if (start_latitude === undefined || start_latitude === null)
            throw new Error("The parameter 'start_latitude' must be defined and cannot be null.");
        else
            url_ += "start_latitude=" + encodeURIComponent("" + start_latitude) + "&"; 
        
        if (start_longitude === undefined || start_longitude === null)
            throw new Error("The parameter 'start_longitude' must be defined and cannot be null.");
        else
            url_ += "start_longitude=" + encodeURIComponent("" + start_longitude) + "&"; 
        
        if (end_latitude === undefined || end_latitude === null)
            throw new Error("The parameter 'end_latitude' must be defined and cannot be null.");
        else
            url_ += "end_latitude=" + encodeURIComponent("" + end_latitude) + "&"; 
        
        if (end_longitude === undefined || end_longitude === null)
            throw new Error("The parameter 'end_longitude' must be defined and cannot be null.");
        else
            url_ += "end_longitude=" + encodeURIComponent("" + end_longitude) + "&";

        return this.http.fetch(url_, {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=UTF-8", 
				"Accept": "application/json; charset=UTF-8"
            }
        }).then((response) => {
            return this.processPrice(response);
        });
    }

    protected processPrice(response: Response): Promise<PriceEstimate[]> {
        return response.text().then((responseText) => {
            const status = response.status; 

            if (status === 200) {
                let result200: PriceEstimate[] = null;
                let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                if (resultData200 && resultData200.constructor === Array) {
                    result200 = [];
                    for (let item of resultData200)
                        result200.push(PriceEstimate.fromJS(item));
                }
                return result200;
            } else {
                let result: Error = null;
                let resultData = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                result = resultData ? Error.fromJS(resultData) : null;
                this.throwException("A server error occurred.", status, responseText, result);
            }
        });
    }

    /**
     * Time Estimates
     * @start_latitude Latitude component of start location.
     * @start_longitude Longitude component of start location.
     * @customer_uuid Unique customer identifier to be used for experience customization.
     * @product_id Unique identifier representing a specific product for a given latitude & longitude.
     * @return An array of products
     */
    time(start_latitude: number, start_longitude: number, customer_uuid: string, product_id: string): Promise<Product[]> {
        let url_ = this.baseUrl + "/estimates/time?";
        if (start_latitude === undefined || start_latitude === null)
            throw new Error("The parameter 'start_latitude' must be defined and cannot be null.");
        else
            url_ += "start_latitude=" + encodeURIComponent("" + start_latitude) + "&"; 
        
        if (start_longitude === undefined || start_longitude === null)
            throw new Error("The parameter 'start_longitude' must be defined and cannot be null.");
        else
            url_ += "start_longitude=" + encodeURIComponent("" + start_longitude) + "&"; 
        
        if (customer_uuid !== undefined)
        
            url_ += "customer_uuid=" + encodeURIComponent("" + customer_uuid) + "&"; 
        
        if (product_id !== undefined)
        
            url_ += "product_id=" + encodeURIComponent("" + product_id) + "&";

        return this.http.fetch(url_, {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=UTF-8", 
				"Accept": "application/json; charset=UTF-8"
            }
        }).then((response) => {
            return this.processTime(response);
        });
    }

    protected processTime(response: Response): Promise<Product[]> {
        return response.text().then((responseText) => {
            const status = response.status; 

            if (status === 200) {
                let result200: Product[] = null;
                let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                if (resultData200 && resultData200.constructor === Array) {
                    result200 = [];
                    for (let item of resultData200)
                        result200.push(Product.fromJS(item));
                }
                return result200;
            } else {
                let result: Error = null;
                let resultData = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                result = resultData ? Error.fromJS(resultData) : null;
                this.throwException("A server error occurred.", status, responseText, result);
            }
        });
    }

    /**
     * User Profile
     * @return Profile information for a user
     */
    me(): Promise<Profile> {
        let url_ = this.baseUrl + "/me";

        return this.http.fetch(url_, {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=UTF-8", 
				"Accept": "application/json; charset=UTF-8"
            }
        }).then((response) => {
            return this.processMe(response);
        });
    }

    protected processMe(response: Response): Promise<Profile> {
        return response.text().then((responseText) => {
            const status = response.status; 

            if (status === 200) {
                let result200: Profile = null;
                let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                result200 = resultData200 ? Profile.fromJS(resultData200) : null;
                return result200;
            } else {
                let result: Error = null;
                let resultData = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                result = resultData ? Error.fromJS(resultData) : null;
                this.throwException("A server error occurred.", status, responseText, result);
            }
        });
    }

    /**
     * User Activity
     * @offset Offset the list of returned results by this amount. Default is zero.
     * @limit Number of items to retrieve. Default is 5, maximum is 100.
     * @return History information for the given user
     */
    history(offset: number, limit: number): Promise<Activities> {
        let url_ = this.baseUrl + "/history?";
        if (offset !== undefined)
        
            url_ += "offset=" + encodeURIComponent("" + offset) + "&"; 
        
        if (limit !== undefined)
        
            url_ += "limit=" + encodeURIComponent("" + limit) + "&";

        return this.http.fetch(url_, {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=UTF-8", 
				"Accept": "application/json; charset=UTF-8"
            }
        }).then((response) => {
            return this.processHistory(response);
        });
    }

    protected processHistory(response: Response): Promise<Activities> {
        return response.text().then((responseText) => {
            const status = response.status; 

            if (status === 200) {
                let result200: Activities = null;
                let resultData200 = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                result200 = resultData200 ? Activities.fromJS(resultData200) : null;
                return result200;
            } else {
                let result: Error = null;
                let resultData = responseText === "" ? null : JSON.parse(responseText, this.jsonParseReviver);
                result = resultData ? Error.fromJS(resultData) : null;
                this.throwException("A server error occurred.", status, responseText, result);
            }
        });
    }

    protected throwException(message: string, status: number, response: string, result?: any): any {
        throw new SwaggerException(message, status, response, result);
    }
}

export class Product { 
    /** Unique identifier representing a specific product for a given latitude & longitude. For example, uberX in San Francisco will have a different product_id than uberX in Los Angeles. */
    product_id: string; 
    /** Description of product. */
    description: string; 
    /** Display name of product. */
    display_name: string; 
    /** Capacity of product. For example, 4 people. */
    capacity: string; 
    /** Image URL representing the product. */
    image: string;

    constructor(data?: any) {
        if (data !== undefined) {
            this.product_id = data["product_id"] !== undefined ? data["product_id"] : null;
            this.description = data["description"] !== undefined ? data["description"] : null;
            this.display_name = data["display_name"] !== undefined ? data["display_name"] : null;
            this.capacity = data["capacity"] !== undefined ? data["capacity"] : null;
            this.image = data["image"] !== undefined ? data["image"] : null;
        }
    }

    static fromJS(data: any): Product {
        return new Product(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["product_id"] = this.product_id !== undefined ? this.product_id : null;
        data["description"] = this.description !== undefined ? this.description : null;
        data["display_name"] = this.display_name !== undefined ? this.display_name : null;
        data["capacity"] = this.capacity !== undefined ? this.capacity : null;
        data["image"] = this.image !== undefined ? this.image : null;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new Product(JSON.parse(json));
    }
}

export class PriceEstimate { 
    /** Unique identifier representing a specific product for a given latitude & longitude. For example, uberX in San Francisco will have a different product_id than uberX in Los Angeles */
    product_id: string; 
    /** [ISO 4217](http://en.wikipedia.org/wiki/ISO_4217) currency code. */
    currency_code: string; 
    /** Display name of product. */
    display_name: string; 
    /** Formatted string of estimate in local currency of the start location. Estimate could be a range, a single number (flat rate) or "Metered" for TAXI. */
    estimate: string; 
    /** Lower bound of the estimated price. */
    low_estimate: number; 
    /** Upper bound of the estimated price. */
    high_estimate: number; 
    /** Expected surge multiplier. Surge is active if surge_multiplier is greater than 1. Price estimate already factors in the surge multiplier. */
    surge_multiplier: number;

    constructor(data?: any) {
        if (data !== undefined) {
            this.product_id = data["product_id"] !== undefined ? data["product_id"] : null;
            this.currency_code = data["currency_code"] !== undefined ? data["currency_code"] : null;
            this.display_name = data["display_name"] !== undefined ? data["display_name"] : null;
            this.estimate = data["estimate"] !== undefined ? data["estimate"] : null;
            this.low_estimate = data["low_estimate"] !== undefined ? data["low_estimate"] : null;
            this.high_estimate = data["high_estimate"] !== undefined ? data["high_estimate"] : null;
            this.surge_multiplier = data["surge_multiplier"] !== undefined ? data["surge_multiplier"] : null;
        }
    }

    static fromJS(data: any): PriceEstimate {
        return new PriceEstimate(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["product_id"] = this.product_id !== undefined ? this.product_id : null;
        data["currency_code"] = this.currency_code !== undefined ? this.currency_code : null;
        data["display_name"] = this.display_name !== undefined ? this.display_name : null;
        data["estimate"] = this.estimate !== undefined ? this.estimate : null;
        data["low_estimate"] = this.low_estimate !== undefined ? this.low_estimate : null;
        data["high_estimate"] = this.high_estimate !== undefined ? this.high_estimate : null;
        data["surge_multiplier"] = this.surge_multiplier !== undefined ? this.surge_multiplier : null;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new PriceEstimate(JSON.parse(json));
    }
}

export class Profile { 
    /** First name of the Uber user. */
    first_name: string; 
    /** Last name of the Uber user. */
    last_name: string; 
    /** Email address of the Uber user */
    email: string; 
    /** Image URL of the Uber user. */
    picture: string; 
    /** Promo code of the Uber user. */
    promo_code: string;

    constructor(data?: any) {
        if (data !== undefined) {
            this.first_name = data["first_name"] !== undefined ? data["first_name"] : null;
            this.last_name = data["last_name"] !== undefined ? data["last_name"] : null;
            this.email = data["email"] !== undefined ? data["email"] : null;
            this.picture = data["picture"] !== undefined ? data["picture"] : null;
            this.promo_code = data["promo_code"] !== undefined ? data["promo_code"] : null;
        }
    }

    static fromJS(data: any): Profile {
        return new Profile(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["first_name"] = this.first_name !== undefined ? this.first_name : null;
        data["last_name"] = this.last_name !== undefined ? this.last_name : null;
        data["email"] = this.email !== undefined ? this.email : null;
        data["picture"] = this.picture !== undefined ? this.picture : null;
        data["promo_code"] = this.promo_code !== undefined ? this.promo_code : null;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new Profile(JSON.parse(json));
    }
}

export class Activity { 
    /** Unique identifier for the activity */
    uuid: string;

    constructor(data?: any) {
        if (data !== undefined) {
            this.uuid = data["uuid"] !== undefined ? data["uuid"] : null;
        }
    }

    static fromJS(data: any): Activity {
        return new Activity(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["uuid"] = this.uuid !== undefined ? this.uuid : null;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new Activity(JSON.parse(json));
    }
}

export class Activities { 
    /** Position in pagination. */
    offset: number; 
    /** Number of items to retrieve (100 max). */
    limit: number; 
    /** Total number of items available. */
    count: number; 
    history: Activity[];

    constructor(data?: any) {
        if (data !== undefined) {
            this.offset = data["offset"] !== undefined ? data["offset"] : null;
            this.limit = data["limit"] !== undefined ? data["limit"] : null;
            this.count = data["count"] !== undefined ? data["count"] : null;
            if (data["history"] && data["history"].constructor === Array) {
                this.history = [];
                for (let item of data["history"])
                    this.history.push(Activity.fromJS(item));
            }
        }
    }

    static fromJS(data: any): Activities {
        return new Activities(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["offset"] = this.offset !== undefined ? this.offset : null;
        data["limit"] = this.limit !== undefined ? this.limit : null;
        data["count"] = this.count !== undefined ? this.count : null;
        if (this.history && this.history.constructor === Array) {
            data["history"] = [];
            for (let item of this.history)
                data["history"].push(item.toJS());
        }
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new Activities(JSON.parse(json));
    }
}

export class Error { 
    code: number; 
    message: string; 
    fields: string;

    constructor(data?: any) {
        if (data !== undefined) {
            this.code = data["code"] !== undefined ? data["code"] : null;
            this.message = data["message"] !== undefined ? data["message"] : null;
            this.fields = data["fields"] !== undefined ? data["fields"] : null;
        }
    }

    static fromJS(data: any): Error {
        return new Error(data);
    }

    toJS(data?: any) {
        data = data === undefined ? {} : data;
        data["code"] = this.code !== undefined ? this.code : null;
        data["message"] = this.message !== undefined ? this.message : null;
        data["fields"] = this.fields !== undefined ? this.fields : null;
        return data; 
    }

    toJSON() {
        return JSON.stringify(this.toJS());
    }

    clone() {
        const json = this.toJSON();
        return new Error(JSON.parse(json));
    }
}

export class SwaggerException extends Error {
    message: string;
    status: number; 
    response: string; 
    result?: any; 

    constructor(message: string, status: number, response: string, result?: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.result = result;
    }
}