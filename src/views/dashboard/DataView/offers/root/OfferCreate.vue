<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in offer.getInputs('text')" :key="param">
          <p class="form-label">
            {{offer.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="offer[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in offer.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{offer.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="offer.getHeaders([param],'first')" class="form-text-input" v-model="offer[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in offer.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{offer.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <ParsingCalendar :disabled="lock(param)" :model="offer[param]" :key="offer[param]" :setter="(value)=>offer.setParam(param, value)" />
        </div>
        <div class="box" v-for="param in offer.getInputs('collections')" :hidden="hide(param)" :key="param">
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
        <div class="box" v-for="param in offer.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{offer.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="offer" :param="param" :disabled="lock(param)" :model="offer"></SelectionManager>
          </div>
        </div>
        <div v-for="param in offer.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
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
import {OfferService} from "@/services/OfferService";
import Toast from "@/utils/classes/Toast";
import OfferCrudController from "@/views/dashboard/DataView/offers/controllers/OfferCrudController";
import OfferClass from "@/views/dashboard/DataView/offers/OfferClass";
import CreateOptions from "@/views/dashboard/DataView/offers/options/CreateOptions";
import OfferLife from "@/views/dashboard/DataView/offers/OfferLife";



export default {
  components: { DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      offer: new OfferService(emitter)
    };
    const toast = new Toast();
    const crud = new OfferCrudController(services.offer, {toast})
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
          history.back()
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



<style>

</style>