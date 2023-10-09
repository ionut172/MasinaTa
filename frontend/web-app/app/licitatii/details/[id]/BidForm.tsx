'use client'

type Props = {
licitatieId: string;
celMaiMareBid: number;
}

import { placeBid } from '@/app/actions/licitatiiActions';
import { numberWithCommas } from '@/app/lib/numberWithCommas';
import { useBidStore } from '@/hooks/useBidStore';
import React from 'react'
import { FieldValues, useForm } from 'react-hook-form';
import { toast } from 'react-hot-toast';

export default function BidForm({licitatieId, celMaiMareBid }: Props) {
    const {register, handleSubmit, reset, formState: {errors}} = useForm();
    const addBid = useBidStore(state => state.addBid);

    function onSubmit(data: FieldValues) {
        if (data.pretRezervare <=celMaiMareBid) {
            reset();
            return toast.error('Trebuie sa fie de cel putin $' + celMaiMareBid + 1)
        }
            
        placeBid(licitatieId, + data.pretRezervare).then(bid => {
            console.log(bid);
            try{
                addBid(bid);
                reset();
            }
            catch(ex){
                console.log(ex);
            }
            
        }).catch(err => toast.error(err.message));
    }

    return (
        <form onSubmit={handleSubmit(onSubmit)} className='flex items-center border-2 rounded-lg py-2'>
            <input 
                type="number" 
                {...register('pretRezervare')}
                className='input-custom text-sm text-gray-600'
                placeholder={`Plaseaza licitatia (minimum bid is $${numberWithCommas(celMaiMareBid + 1)})`}
            />
        </form>
    )
}