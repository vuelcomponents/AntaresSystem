\<script >
import FlexDataPopup from "@/components/popups/FlexDataPopup.vue";
import {AgGridVue} from "ag-grid-vue3";
import GridClass from "@/utils/grid/GridClass";
import collectionManagerMenu from "@/components/CollectionManager/collectionManagerMenu";
import {ClassBunch, ColumnBunch, ServiceBunch} from "@/core/Bunch";
import mainOnCellValueChanged from "@/utils/grid/mainOnCellValueChanged";
import ColManCollection from "@/utils/classes/ColManCollection";
import ColManObject from "@/utils/classes/ColManObject";
import mainOnCellClicked from "@/utils/grid/mainOnCellClicked";
import {
  considerExcludedSelectsFromApiDelete, considerExcludedSelectsFromApiSelect
} from "@/components/CollectionManager/considerExcludedSelectsFromApi";
export default{
  components:{FlexDataPopup, AgGridVue},
  props:{
    modules:Array,
    collection:undefined,
    collectionKey:undefined,
    setCollection:Function,
    // service:undefined,
    emitter:undefined,
    owner:undefined,
    ownerKey:undefined,
    noFixed:undefined,
  },
  data(){
    return{
      instance:new ClassBunch[this.collectionKey](),
      select:null,
      service:null,
      list:[],
      inputCollections:{},
      grid:new GridClass({
            onCellValueChanged: this.onCellValueChanged,
            onCellClicked: this.onCellClicked,
            onSelectionChanged: this.onSelectionChanged,
          },{
            loadList:this.loadList
          }
      ),
      menu:collectionManagerMenu(this.modules, {
        deleteSelected:this.deleteSelected
      })
    }
  },
  mounted(){
    this.service = new ServiceBunch[this.collectionKey](this.emitter)
    this.instance[this.ownerKey] = this.owner.release();
    this.service.getAll().then(res=>{
      if(res.status === 200){
        this.list = res?.data
        console.log('list', this.list)
      }
    })
    Object.keys(this.instance).filter(key=>this.instance.objects.includes(key)).forEach(param=>{
      const objService =  new ServiceBunch[param](this.emitter);
      objService.getAll().then(res=>{
        this.inputCollections[param] = res.data
      })
    })
    console.log(this.instance[this.ownerKey], 'owner')
  },
  methods:{
    loadList(){
      //const list = await this.crud.getAllEmployeeAsync()
      this.grid.api.setRowData(this.collection)
    },
    closeCollection(){
      this.setCollection(null, null)
    },
    onCellValueChanged(params){
      mainOnCellValueChanged(params, {service: this.service})
    },
    onCellClicked(params){
     mainOnCellClicked(params, {service:this.service})
    },
    getColumns(){
      return ColumnBunch[this.collectionKey]()
    },
    deleteSelected(){
      // if(considerExcludedSelectsFromApiDelete(this.owner, this.collectionKey,this.collection,this.grid, this.setCollection)){
      //   return;
      // }
      const colManCollection = new ColManCollection(this.ownerKey, this.owner.id,this.grid.selectedToIds(), this.collectionKey)
      console.log('to delete', colManCollection)
      this.service.deleteMultipleColMan(colManCollection)
          .then(res=>{
            if(res?.status === 200){
              const cleanCollection = this.collection.filter(c=>!this.grid.selected.some(_c=>_c.id === c.id))
              this.setCollection(cleanCollection, this.collectionKey)
              this.grid.api.setRowData(cleanCollection)
            }
      })
    },
    onSelectionChanged(){
     //
    },
    create(){
      // if(considerExcludedSelectsFromApiSelect(this.owner, this.collectionKey,this.collection,this.grid, this.setCollection,this.instance.decycle())){
      //   return;
      // }
      const colManObj = new ColManObject(this.ownerKey, this.owner.id, this.instance.decycle(), this.collectionKey)
      console.log('COL MAN OBJ', colManObj)
      this.save(colManObj)
    },
    addFromList(){
      if(!this.select){
        return;
      }
      // if(considerExcludedSelectsFromApiSelect(this.owner, this.collectionKey,this.collection,this.grid, this.setCollection, this.select)){
      //   return;
      // }
      const colManObj = new ColManObject(this.ownerKey, this.owner.id, this.select, this.collectionKey)
      this.save(colManObj)
    },
    save(colManObj){
      this.service.createColMan(colManObj).then(res=>{
        if(res?.status === 200){
          const newCollection = this.grid.getAllRows() // or = this.collection
          newCollection.push(res.data)
          this.setCollection(newCollection, this.collectionKey)
          this.grid.api.setRowData(newCollection)
        }
      })
    },
    filterInputCollection(inputCollection, param){
      // console.log('kaszana', inputCollection, param)
      if(this.instance.$filters[param] ){
        return this.instance.$filters[param](this.instance, inputCollection,this.owner)
      }
      return inputCollection;
    },
    lock(param){
      if(!this.instance.$lock[param]){
        return false;
      }
      return this.instance.$lock[param]({colman:true})
    },
    hide(param){
      if(!this.instance.$hide[param]){
        return false;
      }
      return this.instance.$hide[param]({colman:true})
    }
  }
}
</script>

<template>
  <FlexDataPopup :close="closeCollection" :no-fixed="noFixed">
    <template #content>
      <MenuBar class="on-dark-bg flex-left noradius" :model="menu"/>
      <div class="col-man-add" v-if="this.modules?.includes('create')">
        <div v-for="param in instance.getInputs('objects')" :key="param">
          <select style="width:95%;color:black" :hidden="hide(param)" class="form-text-input form-text-select" :disabled="lock(param)"  v-model="instance[param]">
            <option v-for="option in filterInputCollection(inputCollections[param], param)" :key="option" :value="option" style="color:black">
              {{option?.code}}
            </option>
          </select>
        </div>
        <div v-for="param in instance.getInputs('text')" :key="param">
          <InputText :hidden="hide(param)" :disabled="lock(param)" style="width:200px;" :placeholder="instance.getHeaders([param],'first')" class="form-text-input" v-model="instance[param]"></InputText>
        </div>
        <div v-for="param in instance.getInputs('numeric')" :key="param">
          <InputNumber :hidden="hide(param)" :disabled="lock(param)" style="width:200px" :placeholder="instance.getHeaders([param],'first')" class="form-text-input" v-model="instance[param]"></InputNumber>
        </div>
        <div v-for="param in instance.getInputs('dates')" :key="param">
          <Calendar  :hidden="hide(param)" :disabled="lock(param)"  date-format="yy-mm-dd" :placeholder="instance.getHeaders([param],'first')"  v-model="instance[param]" class="form-text-input"/>
        </div>
        <div v-for="param in instance.getInputs('files')" :key="param" style="display:flex;align-items:center;">
          <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                      :hidden="hide(param)"
                      :disabled="lock(param)"
                      :chooseLabel="instance.getHeaders([param],'first')" mode="basic" name="demo[]"
                      :maxFileSize="50000000"  @input="instance.setFile($event, param)" />
        </div>

        <button @click="create" class="pimpek">
          <i class="mdi mdi-database-arrow-down" />
        </button>
      </div>
      <div class="col-man-add" v-if="this.modules?.includes('select')" style="padding-inline:12px">
        <select style="width:calc(100% - 55px); color:black" class="form-text-input form-text-select"  v-model="select">
          <option v-for="option in filterInputCollection(list, collectionKey)" :key="option" :value="option" style="color:black;">
            {{option.code ? `${option.code} - ${option?.description}` : (option.firstName ? `${option.firstName} ${option.lastName}`:``)}}
          </option>
        </select>
        <button @click="addFromList" class="pimpek" style="width:50px;">
          <i class="mdi mdi-plus" />
        </button>
      </div>
      <ag-grid-vue
          class="ag-theme-alpine _grid grid-black"
          style="width: 100%;overflow-x:scroll"
          :rowData="grid.data"
          :grid-options="grid.options"
          rowSelection="multiple"
          animateRows="true"
          domLayout="autoHeight"
          :columnDefs="getColumns()"
      >
      </ag-grid-vue>
    </template>
  </FlexDataPopup>
</template>

<style >
.grid{
  display:grid;
}
.g1-1{
  grid-template-columns:1fr 1fr;
}
.gg5{
  grid-gap:5px;
}
.pimpek{
  background:#001c20;
  color:#fff;
  border-radius:4px;
  padding-inline:5px;
}
.col-man-add{
  padding:3px; display:flex; flex-wrap:wrap;gap:5px;
  flex-direction:row;

}
.form-text-select:disabled{
  background:#00000015;
}
.p-component:disabled{
  background:#00000015!important;
}


</style>