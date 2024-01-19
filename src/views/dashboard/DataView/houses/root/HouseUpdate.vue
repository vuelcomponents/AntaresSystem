<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in house.getInputs('text')" :key="param">
          <p class="form-label">
            {{house.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="house[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in house.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{house.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="house.getHeaders([param],'first')" class="form-text-input" v-model="house[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in house.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{house.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <ParsingCalendar :disabled="lock(param)" :model="house[param]" :key="house[param]" :setter="(value)=>house.setParam(param, value)" />
        </div>
        <div class="box" v-for="param in house.getInputs('collections')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{house.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
          </p>
          <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="house[param]"></InputClick>
          <div v-if="life.collectionKey === param">
            <CollectionManager :modules="[
                                   house.create.includes(param) ? 'create' : '',
                                   house.select.includes(param) ? 'select' : '',
                                   house.delete.includes(param) ? 'delete' : '',
                                   house.update.includes(param) ? 'update' :'',
                              ]"
                               :service="services[param]"
                               :owner="house"
                               owner-key="house"
                               :emitter="emitter"
                               :set-collection="setCollection"
                               :collection-key="param"
                               :collection="house[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in house.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{house.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="house" :param="param" :disabled="lock(param)" :model="house"></SelectionManager>
          </div>
        </div>
        <div v-for="param in house.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
          <p class="form-label">
            {{house.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                        :disabled="lock(param)"
                        :chooseLabel="house.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                        :maxFileSize="50000000"  @input="house.setFile($event, param)" />
          </div>
        </div>
        <div  style="display:block">
          <div  :hidden="hide(param)" class="box" v-for="param in house.getInputs('textAreaHeaders',null,true)" :key="param">
            <p class="form-label" >
              {{house.getHeaders([param],"first")}}
            </p>
            <InputText  :disabled="lock(param)" v-model="house[param]" class="form-text-input"/>
          </div>
          <div  :hidden="hide(param)" class="box" v-for="param in house.getInputs('textArea',null,true)" :key="param">
            <p class="form-label" >
              {{house.getHeaders([param],"first")}}
            </p>
            <Editor  :disabled="lock(param)" v-model="house[param]" class="form-text-input"/>
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
import HouseCrudController from "@/views/dashboard/DataView/houses/controllers/HouseCrudController";
import HouseLife from "@/views/dashboard/DataView/houses/HouseLife";
import HouseClass from "@/views/dashboard/DataView/houses/HouseClass";
import EditOptions from "@/views/dashboard/DataView/houses/options/EditOptions";
import {HouseService} from "@/services/HouseService";

export default {
  components: {DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      com: new HouseService(emitter)
    };
    const toast = new Toast();
    const crud = new HouseCrudController(services.com, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      life:new HouseLife(),
      house: new HouseClass(),
      popupOptions: new EditOptions(this.$router, this.update),
    }
  },
  async mounted(){
    this.house = await this.crud.getById(this.$route.params.id)
  },
  methods:{
    update(){
      this.crud.update(this.house).then(res=>{
        if(res?.invalid){
          console.log('invalid house create, empty fields', res.invalid)
        }
        if(res.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.house[key] = collection
      }
    },
    lock(param){
      if(!this.house.$lock[param]){
        return false;
      }
      return this.house.$lock[param]()
    },
    hide(param){
      if(!this.house.$hide[param]){
        return false;
      }
      return this.house.$hide[param]()
    }
  }
}
</script>



<style>

</style>