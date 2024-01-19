

<template>
<div class="multiple-create">
  <div :hidden="hide(param)" class="box" v-for="param in offer.getInputs('text',null,true)" :key="param">
    <p class="form-label">
      {{offer.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="offer[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in offer.getInputs('numeric',null,true)" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{offer.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="offer.getHeaders([param],'first')" class="form-text-input" v-model="offer[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in offer.getInputs('dates',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{offer.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="offer[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in offer.getInputs('collections',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{offer.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="offer[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   offer.create.includes(param) ? 'create' : '',
                                   offer.select.includes(param) ? 'select' : '',
                                   offer.delete.includes(param) ? 'delete' : '',
                                   offer.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="offer"
                         owner-key="offer"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="offer[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in offer.getInputs('objects',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{offer.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="offer" :param="param" :disabled="lock(param)" :model="offer"></SelectionManager>
    </div>
  </div>
  <div v-for="param in offer.getInputs('files',null,true)" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{offer.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="offer.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="offer.setFile($event, param)" />
    </div>
  </div>
  <div class="box" v-for="param in offer.getInputs('booleans')"  :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{offer.getHeaders([param],"first")}} <span class="mini"> | Checks </span>
    </p>
    <div style="display:flex; justify-content:flex-start; width:100%">
      <InputSwitch :disabled="lock(param)"  v-model="offer[param]" ></InputSwitch>
    </div>
  </div>
  <div  style="display:flex; align-items:flex-end;gap:2px; padding:2px; padding-inline:6px;">
    <button class="button-quick" @click="create" style="margin:0;">
      <i class="mdi mdi-link-plus" />
      Create
    </button>
  </div>
</div>
  <div class="multiple-create" style="display:block" :key="life.counterKey">
    <div  :hidden="hide(param)" class="box" v-for="param in offer.getInputs('textArea',null,true)" :key="param">
      <p class="form-label" >
        {{offer.getHeaders([param],"first")}}
      </p>
      <Editor  :disabled="lock(param)" v-model="offer[param]" class="form-text-input"/>
    </div>
  </div>



</template>
<script>

import { inject } from "vue";
import OfferLife from "@/views/dashboard/DataView/offers/OfferLife";
import Toast from "@/utils/classes/Toast";
import OfferCrudController from "@/views/dashboard/DataView/offers/controllers/OfferCrudController";
import OfferClass from "@/views/dashboard/DataView/offers/OfferClass";
import CreateOptions from "@/views/dashboard/DataView/offers/options/CreateOptions";
import {OfferService} from "@/services/OfferService";


export default {
  components: {},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new OfferService(emitter)
    };
    const toast = new Toast()
    const crud = new OfferCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      offer: new OfferClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new OfferLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.offer).then(res=>{
        if(res?.invalid){
          console.log('invalid offer create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          if(res.data) {
            this.emitter.emit('quickOfferAdd', res.data)
          }
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.offer[key] = collection
      }
    },
    lock(param){
      if(!this.offer.$lock[param]){
        return false;
      }
      return this.offer.$lock[param]()
    },
    hide(param){
      if(!this.offer.$hide[param]){
        return false;
      }
      return this.offer.$hide[param]()
    }
  }
}
</script>
<style >

</style>