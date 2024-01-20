<script>
import {OfferService} from "@/services/OfferService";
import DataView from 'primevue/dataview/DataView.vue';

export default {
  name: "RecruitmentScreen",
  components:{DataView},
  setup(){
    const service = new OfferService()
    return {
      service,
    }
  },
  data(){
    return{
      offers:[],
      mappedOffers:[]
    }
  },
  mounted(){
    this.service.getAll().then(res=>{
      if(res.successful()){
        this.offers = res.data;
        this.mappedOffers = this.mapOffers(res.data)
      }
    })
  },
  methods:{
    mapOffers(){
      return this.offers.map(offer => {
        return {
          ...offer,
          title: offer.code
        };
      });
    }
  }
}
</script>

<template>
  <DataView paginator :rows="5" :value="offers">
    <template #list="slotProps">
      <div class="grid grid-nogutter">
        <div v-for="(item, index) in slotProps.items" :key="index" class="col-12">
          <div>
            <img  :src="`data:image/png;base64, ${item.image.fileData}`" :alt="item.name" />
            <div >
              <div >
                <div >{{ item.title }}</div>
                <Rating :modelValue="item.rating" readonly :cancel="false"></Rating>
                <div class="flex align-items-center gap-3">
                                <span class="flex align-items-center gap-2">
                                    <i class="pi pi-tag"></i>
                                    <span class="font-semibold">{{ item.category }}</span>
                                </span>
                  <Tag value="tag" severity="warn"></Tag>
                </div>
              </div>
              <div>
                <span>${{ item.price }}</span>
                <Button icon="pi pi-shopping-cart" rounded :disabled="item.inventoryStatus === 'OUTOFSTOCK'"></Button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </DataView>
</template>

<style scoped lang="scss">

</style>