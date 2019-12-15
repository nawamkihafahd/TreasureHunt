<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Room extends Model
{
    //
	protected $fillable = ['token1', 'token2', 'roomstate', 'started', 'finished'];
	public $timestamps = false;
}
