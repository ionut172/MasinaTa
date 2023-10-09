'use client'

import { getBids } from '@/app/actions/licitatiiActions'
import { useBidStore } from '@/hooks/useBidStore'
import { Licitatii, Bid } from '@/types'
import { User } from 'next-auth'
import React, { useEffect, useState } from 'react'
import { toast } from 'react-hot-toast'
import BidItem from './BidItem'
import { numberWithCommas } from '@/app/lib/numberWithCommas'
import EmptyFilter from '@/app/components/Empty'
import BidForm from './BidForm'
import Header from '@/app/components/Header'

type Props = {
    user: User | null
    licitatii: Licitatii
}

export default function BidList({ user, licitatii }: Props) {
    const [loading, setLoading] = useState(true);
    const bids = useBidStore(state => state.bids);
    const setBids = useBidStore(state => state.setBids);
    const open = useBidStore(state => state.open);
    const setOpen = useBidStore(state => state.setOpen);
    const openForBids = new Date(licitatii.licitatieEnd) > new Date();

    const highBid = bids.reduce((prev, current) => {
        if (current && current.status && current.status.includes('Accepted')) {
            return current.pretRezervare;
        }
        return prev;
    }, 0);
    

    useEffect(() => {
        getBids(licitatii.id)
            .then((res: any) => {
                if (res.error) {
                    throw res.error
                }
                setBids(res as Bid[]);
            }).catch(err => {
                toast.error(err.message);
            }).finally(() => setLoading(false))
    }, [licitatii.id, setLoading, setBids])

    useEffect(() => {
        setOpen(openForBids);
    }, [openForBids, setOpen]);

    if (loading) return <span>Loading licitatii...</span>

    return (
        <div className='rounded-lg shadow-md'>
            <div className='py-2 px-4 bg-white'>
                <div className='sticky top-0 bg-white p-2'>
                    <Header title={`Pretul maxim al licitatiei a ajuns la ${highBid}`} subtitle='Licitatie curenta' />
                </div>
            </div>

            <div className='overflow-auto h-[400px] flex flex-col-reverse px-2'>
                {bids.length === 0 ? (
                    <EmptyFilter title='Nu au fost plasate inca pariuri'
                        subtitle='Incearca acum sa adaugi o licitatie.' />
                ) : (
                    <>
                        {bids.map(bid => (
                            <BidItem key={bid.id} bid={bid} />
                        ))}
                    </>
                )}
            </div>
            
            <div className='px-2 pb-2 text-gray-500'>
                
                {!open ? (
                    <div className='flex items-center justify-center p-2 text-lg font-semibold'>
                        Licitatia a luat sfarsit.
                    </div>
                ) : !user ? (
                    <div className='flex items-center justify-center p-2 text-lg font-semibold'>
                        Logheaza-te pentru a paria.
                    </div>
                ) : user && user.username === licitatii.vanzator ? (
                    <div className='flex items-center justify-center p-2 text-lg font-semibold'>
                        Nu poti plasa bid pe licitatia ta.
                    </div>
                ) : (
                    <BidForm licitatieId={licitatii.id} celMaiMareBid={highBid} />
                )}
            </div>
        </div>
    )
}