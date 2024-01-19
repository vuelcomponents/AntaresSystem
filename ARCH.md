
![nobglogo.png](src%2Fassets%2Fstacked%2Fnobglogo.png)
----------------------
-------------------
# Antares
### CRUD & DATA MANAGEMENT

 
----------------------------
----------------------

# ModelClass

The `ModelClass` serves as a foundational class that encapsulates essential functionalities for working with data in a modular and organized manner. This class is designed to be inherited by specific model classes, such as `CarClass`, which specialize in distinct aspects of data processing and analysis.

## Features

1. **Data Parsing:**
    - The `ModelClass` includes robust methods for parsing diverse data sources. Whether handling structured or unstructured data, these parsing functions facilitate seamless integration and extraction of relevant information.

2. **Data Grouping:**
    - Efficient data grouping functions are integrated into the `ModelClass`. These methods enable the categorization and organization of data based on specified parameters, streamlining subsequent analyses and operations.

3. **Data Processing Functions:**
    - Various functions within the `ModelClass` are dedicated to performing essential tasks related to data manipulation, transformation, and enhancement. These functions are inheritable by subclasses, ensuring a consistent and standardized approach to data processing.

4. **Extensibility:**
    - The design of the `ModelClass` promotes extensibility, allowing developers to create specialized model classes (e.g., `CarClass`) that inherit and build upon the core functionalities. This facilitates a modular and scalable structure for handling different types of data models.

--------------------------------------

# SomeModelClass extends ModelClass

The `SomeModelClass` extends the `ModelClass` to provide specialized functionality for handling financial transaction data. It encapsulates a list of operational fields, categorizing them into objects representing various data types.

## Operational Fields:

1. **Textual Data:**
    - *Field:* `text`
    - *Type:* Text
    - *Header:* Transaction Description

2. **Numerical Data:**
    - *Field:* `numbers`
    - *Type:* Numeric
    - *Header:* Transaction Amount

3. **Date Data:**
    - *Field:* `dates`
    - *Type:* Date
    - *Header:* Transaction Date

4. **Object-Oriented Data:**
    - *Field:* `objects`
    - *Type:* Object
    - *Header:* Customer Information
    - *Class:* CustomerInfoClass (hypothetical class representing customer details)

5. **Collection Data:**
    - *Field:* `collections`
    - *Type:* Collection
    - *Header:* Item List
    - *Class:* ItemClass (hypothetical class representing individual items in the transaction)

## Additional Features:

- **Required Fields:**
    - The `SomeModelClass` designates certain fields as required for a complete representation of financial transactions.

- **Data Manipulation Services:**
    - Provides services for manipulating nested data within the class, ensuring efficient and organized data handling.

## Example Usage:

```js
class {
    validate(options){
        ...
    }
    getHeaders(props, option){
       ...
    }

    release(){
        ...
    }
    decycle() {
        ...
    }
    ...
}
class SomeModelClass extends ModelClass{
    id;
    variant;
    ...,
    headers = {
        id: "Id",
        variant:"Variant",
        ...
    }
    text= [...]
    required = [...]
    dates = [...]
    collections = [
        'employees',
        'companies'
    ]
    objects = [
        'variant',
        ...
    ]
}

```

---------------------------------
# Bunch.js: ServiceBunch, ClassBunch

The `ServiceBunch` object serves as a mapping between parameter names and corresponding service classes, facilitating generic handling of requests and data retrieval. Each parameter corresponds to a specific type of data service. Here is a breakdown of the parameters:

- `realisations` and `realisation`: Mapped to the `RealisationService` class, responsible for handling requests related to realisations.

- `variantRealisations` and `variantRealisation`: Mapped to the `VariantRealisationService` class, which manages requests for variant realisations.

- .......
## ClassBunch

The `ClassBunch` object is a mapping between parameter names and the corresponding class implementations. It is designed for generic data handling and serves as a reference for creating instances of different classes based on parameter names. Here's a breakdown of the parameters:

- `realisations` and `realisation`: Mapped to the `RealisationClass`, representing the class for managing realisations.

- `variantRealisations` and `variantRealisation`: Mapped to the `VariantRealisationClass`, representing the class for handling variant realisations.

- ....
- ## ColumnBunch

The `ColumnBunch` object is a mapping between parameter names and grid columns:

- `realisations` and `realisation`: Mapped to the `RealisationColumns`, representing the collection of columns.

- `variantRealisations` and `variantRealisation`: Mapped to the `VariantRealisationColumns`,

- ....
````js

const ServiceBunch = {
   realisations:RealisationService,
   ...
}
const ClassBunch = {
   realisations:RealisationClass,
   ...
}
const ColumnBunch = {
    realisations:ReasliationColumns,
    ....
}
export {ServiceBunch,ClassBunch,ColumnBunch}
````

--------------------------

# Service

The `Service` class is designed as a base class for handling various data services. It encompasses request interceptors and **predefined paths that can be substituted with generic placeholders** in its **inheriting classes**.
<span style="color:#008b8b">**This architecture allows for flexible customization and reuse of service functionality.**</span>

## Features:

### 1. Interceptor for Requests

The `Service` class incorporates an interceptor mechanism for requests. This enables the interception and manipulation of requests before they are sent, providing a centralized point for adding custom logic such as headers, authentication, or logging.

### 2. Defined Paths with Generic Placeholders

The class defines specific paths (GETALL, GET, PATCH, DELETE, CREATE) related to the data service it represents. **These paths, however, include generic placeholders that can be dynamically substituted by inheriting classes based on their specific requirements. This design promotes code reusability and adaptability to <span style="color:#008b8b">different data entities.</span>**

### 3. Nullable emitter

Emitter activates toasting errors from API

````js
class EmployeeService extends WpService {
    constructor(emitter) {
        super(emitter);
        this.pathName = 'employee'
    }
}
````

--------------------

# SomeCrudController

The `SomeCrudController` class acts as an intermediary between the view layer and the underlying service, handling data parsing, validation, and related operations. This controller is specifically tailored for employee data and includes methods for retrieving, creating, updating, and deleting employee records.

## Constructor:

- **Parameters:**
   - `service`: An instance of the service class responsible for handling CRUD operations on employee data.
   - `options`: Additional options that can be passed to customize the behavior of the controller, such as displaying toasts for validation messages.

## Properties:

- `service`: The instance of the service class associated with this controller.
- `options`: Additional options that customize the behavior of the controller.

## Methods:

1. **`getAllEmployeeAsync()`**
   - **Description:** Retrieves all employee records asynchronously.
   - **Return:** A promise that resolves to the list of all employees.

2. **`getEmployeeByIdAsync(id)`**
   - **Description:** Retrieves an employee by ID asynchronously.
   - **Parameters:**
      - `id`: The unique identifier of the employee.
   - **Return:** A promise that resolves to an `EmployeeClass` instance populated with the retrieved employee data.

3. **`createEmployeeAsync(employee)`**
   - **Description:** Creates a new employee asynchronously, performing data validation and formatting.
   - **Parameters:**
      - `employee`: The employee data to be created.
   - **Return:** A promise that resolves to the result of the creation operation.

4. **`updateEmployeeAsync(employee)`**
   - **Description:** Updates an existing employee asynchronously, performing data validation and formatting.
   - **Parameters:**
      - `employee`: The updated employee data.
   - **Return:** A promise that resolves to the result of the update operation.

5. **`deleteEmployeeAsync(id)`**
   - **Description:** Deletes an employee by ID asynchronously.
   - **Parameters:**
      - `id`: The unique identifier of the employee to be deleted.
   - **Return:** A promise that resolves to the result of the delete operation.

6. **`deleteMultipleEmployeeAsync(employees)`**
   - **Description:** Deletes multiple employees asynchronously.
   - **Parameters:**
      - `employees`: An array of employee data to be deleted.
   - **Return:** A promise that resolves to the result of the delete operation.

7. **`saveEmployeeChanges()`**
   - **Description:** Placeholder method for saving employee changes. This method can be extended for custom behavior.
   - **Note:** Currently, this method logs a message to the console.

## Usage Example:

```javascript
import moment from "moment";
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";

export default class SomeCrudController {
    service = undefined;
    options = null;

    constructor(service, options) {
        this.service = service;
        this.options = options;
    }

    // ... (Other methods described above)

    async saveEmployeeChanges(){
        console.log('save changes');
    }
}
```
------------------------------
# CollectionManager.vue
`CollectionManager` component is intentionally designed to be **highly adaptable, allowing it to generically interact with any collection and all objects managed by generic services and classes**. The architecture promotes reusability and extensibility, making it a powerful tool for handling diverse data scenarios within the Vue.js application.

- **Data Initialization:**
   - The component initializes several data properties, including `instance`, `select`, `service`, `list`, `inputCollections`, and `grid`.
   - The `instance` represents an instance of a class specific to the current collection type, dynamically instantiated from the `ClassBunch`.
   - The `service` is an instance of a service class associated with the current collection, dynamically instantiated from the `ServiceBunch`.

- **Props:**
   - `modules`: An array of modules specifying the available functionalities (e.g., 'create', 'select').
   - `collection`: The current collection being managed by the component.
   - `collectionKey`: The key identifying the name of collection.
   - `setCollection`: A function to set the current collection and collection key.
   - `emitter`: An emitter object for handling communication.
   - `owner`: The owner object associated with the collection.
   - `ownerKey`: The key identifying the name of owner.

### Usage:
```vue
  <div class="box" v-for="param in Object.keys(variant).filter(key=>variant.collections?.includes(key))" :key="param">
   <p class="form-label">
      {{variant.getHeaders([param],"first")}} <span class="mini"> | Date </span>
   </p>
   <InputClick format="collection" @click="setCollection(null, param)" :value="variant[param]"></InputClick>
   <div v-if="life.collectionKey === param">
      <CollectionManager :modules="['create', 'delete', 'update']"
                         :service="services[param]"
                         :owner="variant"
                         owner-key="variant"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="variant[param]"></CollectionManager>
   </div>
</div>

```
