using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public GameObject goal;
    public float speed = 2.0f;

    // each frame, also we can use deltaTime to define equal movement that won't depend on FPS
    void Update()
    {
        // we need direction vector to define, in which way we need to push our pig, normalizataion we need to make it accurate
        // most of the vectors can define different numbers, but from the math rules, the "good" direction length vector must in range from -1 to 1
        Vector3 direction = (goal.transform.position - transform.position).normalized;

        // this draws line (distance between A and B)
        // we need it to prevent pig "Convulsions" because it can oversteep designated point for 0.00001
        // and then its gonna try to step back again and it will be looped, so our pig will be doomed forever.
        float distance = Vector3.Distance(transform.position, goal.transform.position);

        if (distance > 0.1f)
        {
            // this tbh was written by AI, i'm not gonna lie i just want my pig to look at the target as normal pig.
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

            // now we have this:
            // our pig position = direction vector * pig speed * delta time
            // let me simplify the explanation:
            // each frame we move our pig position in the way of direction * time (time because our pig will be flying if u had a High-end PC)
            transform.position += direction * speed * Time.deltaTime;
            // also, there is a lot of ways to move object in Unity
            // transform,translate,MoveTowards,Lerp,AddForce but i used the simplist one, the one that i truly now hot it works.
        }
    }
}
// !!!!!!!!!!!!!!
// Also i'd like to clarify my implementation for this task. The tutorial link provided (in classroom) contained the theoretical concepts of Vectors and Direction Vectors,
// it doesn't actually "list" a specific 'homework or some sort of a task to complete.
// That's why i decided to build a script based on video-material.
// Also i'd like to say that im not a Unity expert, before (this)Unity course i spent some time to learn different Unity things,
// and vectors/dot product was one of this things.
// Also i'd like to explain what was meant in provided tutorial:
// there was listed this formula sqrt(x^2 - y^2) , it usually depends how much dimension we're gonna use, if it'll be 2D Dimmension
// the formula will be SQRT( (first2 - first1)^2 + (last2 - last1)^2 )
// so we're just finding difference multiplying it by 2 (i meant sqrt) , summing it up and then extract number of founded sqrt root.
// The more clear explanation:
// lets assume pig staying at 0,0 and goal at 4,5 from tutorial so:
// As long as starting pig cordinates 0,0 then we need to start from this value so we have this order:
// (2D , X,Z:
// 0,0,4,5 where:
// x1,y1,x2,y2
// so lets pass it to formula:
// (0 - 4)^2 + (0 - 5) ^ 2
// we get: 4 * 4 + 5 * 5 = 41
// sqr(41) =~ 6.4 so this will be approximate distance between pig and goal target.