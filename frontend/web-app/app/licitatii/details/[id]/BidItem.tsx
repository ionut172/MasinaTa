import { numberWithCommas } from '@/app/lib/numberWithCommas';
import { Bid } from '@/types';
import { format } from 'date-fns';
import React from 'react';

type Props = {
    bid: Bid;
};

export default function BidItem({ bid }: Props) {
    const bidTime = bid.bidTime;

// Format the date
const formattedDate = new Intl.DateTimeFormat('en-US', {
  day: '2-digit',
  month: 'short',
  year: 'numeric',
  hour: 'numeric',
  minute: 'numeric',
  hour12: true,
}).format(new Date(bidTime));

// Replace the existing JSX with the formatted date within the span
const oraUpdatata = (
  <span className='text-gray-700 text-sm'>
    {formattedDate}
  </span>
);

// Now use updatedJSX wherever you need it in your React components.

    function getBidInfo() {
        let bgColor = '';
        let text = '';
        switch (bid.status) {
            case 'Accepted':
                bgColor = 'bg-green-200';
                text = 'Acceptat';
                break;
            case 'AcceptedBelowReserve':
                bgColor = 'bg-amber-500';
                text = 'Rezervat';
                break;
            case 'TooLow':
                bgColor = 'bg-red-200';
                text = 'Bid prea mic';
                break;
            default:
                bgColor = 'bg-red-200';
                text = 'Pariu plasat dupa incheierea licitatiei.';
                break;
        }
        return { bgColor, text };
    }

    return (
        <div
            className={`
            border-gray-300 border-2 px-3 py-2 rounded-lg
            flex justify-between items-center mb-2
            ${getBidInfo().bgColor}
        `}
        >
            <div className='flex flex-col'>
                <span>Licitatie facuta de: {bid.vanzator} </span>
            </div>
            <div className='flex flex-col text-right'>
                <div className='text-xl font-semibold'>
                    {bid.pretRezervare} euro
                </div>
                
                <span className='text-gray-700 text-sm'>
                    {oraUpdatata}

                </span>
                <div className='flex flex-row items-center'>
                    <span>{getBidInfo().text}</span>
                </div>
            </div>
        </div>
    );
}
