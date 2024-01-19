<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in car.getInputs('text')" :key="param">
          <p class="form-label">
            {{car.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="car[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in car.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{car.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="car.getHeaders([param],'first')" class="form-text-input" v-model="car[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in car.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{car.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <ParsingCalendar :disabled="lock(param)" :model="car[param]" :key="car[param]" :setter="(value)=>car.setParam(param, value)" />
        </div>
        <div class="box" v-for="param in car.getInputs('collections')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{car.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
          </p>
          <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="car[param]"></InputClick>
          <div v-if="life.collectionKey === param">
            <CollectionManager :modules="[
                                   car.create.includes(param) ? 'create' : '',
                                   car.select.includes(param) ? 'select' : '',
                                   car.delete.includes(param) ? 'delete' : '',
                                   car.update.includes(param) ? 'update' :'',
                              ]"
                               :service="services[param]"
                               :owner="car"
                               owner-key="car"
                               :emitter="emitter"
                               :set-collection="setCollection"
                               :collection-key="param"
                               :collection="car[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in car.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{car.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="car" :param="param" :disabled="lock(param)" :model="car"></SelectionManager>
          </div>
        </div>
        <div v-for="param in car.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
          <p class="form-label">
            {{car.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                        :disabled="lock(param)"
                        :chooseLabel="car.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                        :maxFileSize="50000000"  @input="car.setFile($event, param)" />
          </div>
        </div>
        <div  style="display:block">
          <div  :hidden="hide(param)" class="box" v-for="param in car.getInputs('textAreaHeaders',null,true)" :key="param">
            <p class="form-label" >
              {{car.getHeaders([param],"first")}}
            </p>
            <InputText  :disabled="lock(param)" v-model="car[param]" class="form-text-input"/>
          </div>
          <div  :hidden="hide(param)" class="box" v-for="param in car.getInputs('textArea',null,true)" :key="param">
            <p class="form-label" >
              {{car.getHeaders([param],"first")}}
            </p>
            <Editor  :disabled="lock(param)" v-model="car[param]" class="form-text-input"/>
          </div>
        </div>
      </template>
      <template v-slot:item2contentLeft>
        <div class="box">
          left
        </div>
      </template>

      <template v-slot:item2contentRight>
        <div class="box">
          right
        </div>
      </template>

      <template v-slot:item3contentLeft>
        <div class="box">
          left
        </div>
      </template>

      <template v-slot:item3contentRight>
        <div class="box">
          right
        </div>
      </template>
    </DataPopup>
  </div>
</template>

<script>

import DataPopup from "@/components/popups/DataPopup.vue";

import { inject } from "vue";
import Toast from "@/utils/classes/Toast";
import CarCrudController from "@/views/dashboard/DataView/cars/controllers/CarCrudController";
import CarLife from "@/views/dashboard/DataView/cars/CarLife";
import CarClass from "@/views/dashboard/DataView/cars/CarClass";
import EditOptions from "@/views/dashboard/DataView/cars/options/EditOptions";
import {CarService} from "@/services/CarService";

export default {
  components: {DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      com: new CarService(emitter)
    };
    const toast = new Toast();
    const crud = new CarCrudController(services.com, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      life:new CarLife(),
      car: new CarClass(),
      popupOptions: new EditOptions(this.$router, this.update),
    }
  },
  async mounted(){
    this.car = await this.crud.getById(this.$route.params.id)
  },
  methods:{
    update(){
      this.crud.update(this.car).then(res=>{
        if(res?.invalid){
          console.log('invalid car create, empty fields', res.invalid)
        }
        if(res.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.car[key] = collection
      }
    },
    lock(param){
      if(!this.car.$lock[param]){
        return false;
      }
      return this.car.$lock[param]()
    },
    hide(param){
      if(!this.car.$hide[param]){
        return false;
      }
      return this.car.$hide[param]()
    }
  }
}
</script>



<style>

</style>