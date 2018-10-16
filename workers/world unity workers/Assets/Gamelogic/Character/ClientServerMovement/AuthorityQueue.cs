/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthorityQueue {
    private struct AuthorityWithLife
    {
        public float aliveFor;
        public AuthoritativeTransform authorityTransform;

        public AuthorityWithLife(AuthoritativeTransform authorityTransform)
        {
            aliveFor = 0;
            this.authorityTransform = authorityTransform;
        }

        public AuthorityWithLife(AuthoritativeTransform authorityTransform, float aliveFor)
        {
            this.aliveFor = aliveFor;
            this.authorityTransform = authorityTransform;
        }
    }

    private List<AuthorityWithLife?> _queue = new List<AuthorityWithLife?>();

    public void Tick(float deltaTime)
    {
        for(int i = 0; i < _queue.Count; i++)
        {
            AuthorityWithLife authorityWithLife = _queue[i];
            if(authorityWithLife.authorityTransform != null) { }
            _queue[i] = new AuthorityWithLife(authorityWithLife.authorityTransform, authorityWithLife.aliveFor + deltaTime);
        }
    }

    public float PeekAliveFor()
    {
        return _queue[0].aliveFor; //Struct so will be initialised to 0. This is fine.
    }

    public void Insert(AuthoritativeTransform authorityTransform)
    {
        _queue.Add(new AuthorityWithLife(authorityTransform));
    }

    public AuthoritativeTransform Pop()
    {
        AuthoritativeTransform authorityTransform = _queue[0].authorityTransform;
        _queue.RemoveAt(0);
        return authorityTransform;
    }

    //Remove all authority transforms at timestamp or after
    //Useful when the server has overshot the canonical move because it received inputs late
    //Assumes they are in ascending chronological order (a sensible assumption!)
    public void PurgeAfterTimestamp(float timestamp)
    {
        for (int i = _queue.Count - 1; i >= 0; i--)
        {
            AuthoritativeTransform authorityTransform = _queue[i].authorityTransform;
            if (authorityTransform.timestamp > timestamp)
            {
                _queue.RemoveAt(i);
            }
            else
            {
                break;
            }
        }
    }

    //Getting rid of everything is easier. We don't have to deal with unpublished transforms
    //That exist for an input we have discarded
    public void Purge()
    {
        _queue = new List<AuthorityWithLife?>();
    }
}*/
